import { inject, Injectable, signal } from "@angular/core";
import { Store } from "@ngrx/store";
import { toggleSidebar } from "./actions/sidebar.actions";
import { selectSidebarStatus } from "./selectors/sidebar.selectors";
import * as QueueActions from "./actions/queue.actions";
import { selectData, selectError, selectLoadingStatus } from "./selectors/queue.selectors";
import { DataService } from "./services/dataservice.service";

@Injectable({
  providedIn: "root"
})
export class QueueFacade {
  store = inject(Store);
  dataService = inject(DataService);

  isSidebarOpen$ = this.store.selectSignal(selectSidebarStatus);
  loading$ = this.store.selectSignal(selectLoadingStatus);
  error$ = this.store.selectSignal(selectError);
  data$ = this.store.selectSignal(selectData);
  tasks$ = this.dataService.tasks$;

  activeTab = signal<'QUEUES' | 'TASKS' | 'SERVERS' | 'POSTGRES'>('QUEUES');

  setActiveTab(tab: 'QUEUES' | 'TASKS' | 'SERVERS' | 'POSTGRES') {
    this.activeTab.set(tab);
  }

  handleSidebarToggle() {
    this.store.dispatch(toggleSidebar());
  }

  loadData(id: string): void {
    this.store.dispatch(QueueActions.loadPage({ id }));
  }

  submitTask(type: string, payload: any, shouldFail: boolean = false): void {
    this.dataService.submitNewTask(type, payload, shouldFail);
    this.loadData('default');
  }

  retryTask(taskId: string): void {
    this.dataService.retryTask(taskId);
    this.loadData('default');
  }
}