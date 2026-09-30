import { Component, inject } from '@angular/core';
import { QueueFacade } from '../../state/queue-facade';

@Component({
  selector: 'app-sidebar',
  imports: [],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent {
  queueFacade = inject(QueueFacade);
}
