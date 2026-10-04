import { Component, OnInit, OnDestroy, inject, signal, computed } from '@angular/core';
import { QueueFacade } from '../../state/queue-facade';
import { Task } from '../../state/interfaces/task';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-queue',
  imports: [],
  templateUrl: './queue.component.html',
  styleUrl: './queue.component.css',
})
export class QueueComponent implements OnInit, OnDestroy {
  queueFacade = inject(QueueFacade);
  loading$ = this.queueFacade.loading$;
  error$ = this.queueFacade.error$;

  tasks = signal<Task[]>([]);
  activeFilter = signal<'ALL' | 'PENDING' | 'RUNNING' | 'COMPLETED' | 'FAILED'>('ALL');
  searchQuery = signal<string>('');
  selectedTask = signal<Task | null>(null);
  isSubmitModalOpen = signal<boolean>(false);
  isQueuePaused = signal<boolean>(false);
  copiedTaskId = signal<string | null>(null);

  newTaskType = signal<string>('OrderInventoryReserve');
  newTaskPayload = signal<string>('{\n  "orderId": "ORD-5501",\n  "amount": 250.00\n}');
  newTaskShouldFail = signal<boolean>(false);

  private sub?: Subscription;

  filteredTasks = computed(() => {
    const list = this.tasks();
    const filter = this.activeFilter();
    const query = this.searchQuery().trim().toLowerCase();

    return list.filter((t) => {
      const matchesFilter = filter === 'ALL' || t.status === filter;
      const matchesQuery =
        !query ||
        t.id.toLowerCase().includes(query) ||
        (t.type && t.type.toLowerCase().includes(query));
      return matchesFilter && matchesQuery;
    });
  });

  stats = computed(() => {
    const list = this.tasks();
    return {
      total: list.length,
      pending: list.filter((t) => t.status === 'PENDING').length,
      running: list.filter((t) => t.status === 'RUNNING').length,
      completed: list.filter((t) => t.status === 'COMPLETED').length,
      failed: list.filter((t) => t.status === 'FAILED').length,
    };
  });

  errorRate = computed(() => {
    const s = this.stats();
    return s.total > 0 ? ((s.failed / s.total) * 100).toFixed(1) : '0.0';
  });


  ngOnInit(): void {
    this.queueFacade.loadData('default-queue');
    this.sub = this.queueFacade.tasks$.subscribe((data) => {
      this.tasks.set(data);
      const currentSelected = this.selectedTask();
      if (currentSelected) {
        const updated = data.find((t) => t.id === currentSelected.id);
        if (updated) {
          this.selectedTask.set(updated);
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

  setFilter(status: 'ALL' | 'PENDING' | 'RUNNING' | 'COMPLETED' | 'FAILED') {
    this.activeFilter.set(status);
  }

  onSearchInput(event: Event) {
    const target = event.target as HTMLInputElement;
    this.searchQuery.set(target.value);
  }

  openDetails(task: Task) {
    this.selectedTask.set(task);
  }

  closeDetails() {
    this.selectedTask.set(null);
  }

  openSubmitModal() {
    this.isSubmitModalOpen.set(true);
  }

  closeSubmitModal() {
    this.isSubmitModalOpen.set(false);
  }

  toggleQueuePause() {
    this.isQueuePaused.update((paused) => !paused);
  }

  copyTaskId(id: string, event: Event) {
    event.stopPropagation();
    navigator.clipboard.writeText(id);
    this.copiedTaskId.set(id);
    setTimeout(() => {
      if (this.copiedTaskId() === id) {
        this.copiedTaskId.set(null);
      }
    }, 2000);
  }

  onTypeChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    this.newTaskType.set(target.value);
  }

  onPayloadChange(event: Event) {
    const target = event.target as HTMLTextAreaElement;
    this.newTaskPayload.set(target.value);
  }

  onFailToggle(event: Event) {
    const target = event.target as HTMLInputElement;
    this.newTaskShouldFail.set(target.checked);
  }

  confirmSubmit() {
    let parsed: any = {};
    try {
      parsed = JSON.parse(this.newTaskPayload());
    } catch {
      parsed = { raw: this.newTaskPayload() };
    }

    this.queueFacade.submitTask(
      this.newTaskType(),
      parsed,
      this.newTaskShouldFail()
    );
    this.closeSubmitModal();
  }

  retryTask(taskId: string) {
    this.queueFacade.retryTask(taskId);
  }

  formatJson(val: any): string {
    try {
      return JSON.stringify(val, null, 2);
    } catch {
      return String(val);
    }
  }
}
