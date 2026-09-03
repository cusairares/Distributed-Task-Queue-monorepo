import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideStore } from '@ngrx/store';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { sidebarReducer } from '../state/reducers/sidebar.reducers';
import { provideEffects } from '@ngrx/effects';
import { queueReducer } from '../state/reducers/queue.reducers';
import { QueueEffects } from '../state/effects/queue.effects';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideStore({
      sidebar: sidebarReducer,
      queue: queueReducer,
    }),
    provideEffects(QueueEffects),
    provideStoreDevtools({
      maxAge: 25,
      logOnly: false,
      autoPause: true,
      trace: false,
      traceLimit: 75,
      connectInZone: true,
    }),
  ],
};
