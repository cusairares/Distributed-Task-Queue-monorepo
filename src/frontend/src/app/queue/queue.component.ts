import { Component, OnInit, inject } from '@angular/core';
import { QueueFacade } from '../../state/queue-facade';

@Component({
  selector: 'app-queue',
  imports: [],
  templateUrl: './queue.component.html',
  styleUrl: './queue.component.css',
})
export class QueueComponent implements OnInit {
  queueFacade = inject(QueueFacade);
  loading$ = this.queueFacade.loading$;
  error$ = this.queueFacade.error$;

  ngOnInit(): void {
    this.queueFacade.loadData('target-id');
  }
}
