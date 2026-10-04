import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, of } from "rxjs";
import { QueueData } from "../interfaces/queue-data";
import { Task } from "../interfaces/task";

@Injectable({
  providedIn: 'root',
})
export class DataService {
  private initialTasks: Task[] = [
    {
      id: 'task-8f92a1',
      type: 'OrderInventoryReserve',
      status: 'COMPLETED',
      payload: { orderId: 'ORD-9821', sku: 'LAPTOP-PRO-16', quantity: 1, warehouse: 'EU-CENTRAL-1' },
      max_retries: 3,
      retries_count: 0,
      fencing_token: 1042,
      created_at: '2m ago',
      execution_duration_ms: 245
    },
    {
      id: 'task-3c41b8',
      type: 'ProcessPaymentCharge',
      status: 'RUNNING',
      payload: { customerId: 'CUST-4102', amount: 1499.00, currency: 'USD', paymentMethod: 'card_token_xyz' },
      max_retries: 3,
      retries_count: 1,
      fencing_token: 1043,
      created_at: '35s ago',
      execution_duration_ms: 120
    },
    {
      id: 'task-1d90ef',
      type: 'GenerateInvoicePdf',
      status: 'PENDING',
      payload: { invoiceId: 'INV-2026-004', template: 'vat_standard', recipientEmail: 'billing@client.com' },
      max_retries: 2,
      retries_count: 0,
      fencing_token: 1044,
      created_at: '12s ago'
    },
    {
      id: 'task-9a72cc',
      type: 'WebhookNotificationDispatch',
      status: 'FAILED',
      payload: { endpoint: 'https://api.partner.io/events', event: 'order.fulfilled', attempt: 3 },
      max_retries: 3,
      retries_count: 3,
      fencing_token: 1041,
      created_at: '5m ago',
      execution_duration_ms: 5012,
      error_message: 'HTTP 504 Gateway Timeout: Remote webhook endpoint failed to respond within 5000ms deadline.'
    }
  ];

  private tasksSubject = new BehaviorSubject<Task[]>(this.initialTasks);
  tasks$ = this.tasksSubject.asObservable();

  fetchQueueData(id: string): Observable<QueueData> {
    const currentTasks = this.tasksSubject.value;
    const mock: QueueData = {
      name: 'default-queue',
      state: 'RUN',
      size: currentTasks.length,
      memory_usage: 42,
      processed: currentTasks.filter(t => t.status === 'COMPLETED').length,
      failed: currentTasks.filter(t => t.status === 'FAILED').length,
      error_rate: 1.2,
      tasks: currentTasks
    };
    return of(mock);
  }

  submitNewTask(type: string, payload: any, shouldFail: boolean = false): void {
    const newTask: Task = {
      id: 'task-' + Math.random().toString(36).substring(2, 8),
      type: type,
      status: shouldFail ? 'FAILED' : 'PENDING',
      payload: payload,
      max_retries: 3,
      retries_count: shouldFail ? 3 : 0,
      fencing_token: Math.floor(1000 + Math.random() * 9000),
      created_at: 'Just now',
      execution_duration_ms: shouldFail ? 420 : 18,
      error_message: shouldFail ? 'IntentionalFailureException: Simulated pipeline rejection per Sprint 1 QA scenario.' : undefined
    };

    const updated = [newTask, ...this.tasksSubject.value];
    this.tasksSubject.next(updated);
  }

  retryTask(taskId: string): void {
    const current = this.tasksSubject.value.map(t => {
      if (t.id === taskId) {
        return {
          ...t,
          status: 'RUNNING' as const,
          retries_count: t.retries_count + 1,
          error_message: undefined
        };
      }
      return t;
    });
    this.tasksSubject.next(current);

    setTimeout(() => {
      const finished = this.tasksSubject.value.map(t => {
        if (t.id === taskId) {
          return {
            ...t,
            status: 'COMPLETED' as const,
            execution_duration_ms: 180
          };
        }
        return t;
      });
      this.tasksSubject.next(finished);
    }, 1200);
  }
}