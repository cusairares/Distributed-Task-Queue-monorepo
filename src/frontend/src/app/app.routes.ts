import { Routes } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { QueueComponent } from './queue/queue.component';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'queue',
  },
  {
    path: 'queue',
    component: QueueComponent,
  },
  {
    path: '**',
    component: NotFoundComponent,
  },
];
