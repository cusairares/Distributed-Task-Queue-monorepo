import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from './sidebar/sidebar.component';
import { QueueFacade } from '../state/queue-facade';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, SidebarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  protected readonly title = signal('Frontend');

  queueFacade = inject(QueueFacade);

  onSidebarToggle() {
    this.queueFacade.handleSidebarToggle();
  }
}
