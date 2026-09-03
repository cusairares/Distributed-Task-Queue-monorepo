import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideState, provideStore } from '@ngrx/store';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { sidebarReducer } from '../state/reducers/sidebar.reducers';
import { provideEffects } from '@ngrx/effects';
import { queueReducer } from '../state/reducers/queue.reducers';
import { QueueEffects } from '../state/effects/queue.effects';

export const appConfig: ApplicationConfig = {
  providers: [
    provideStoreDevtools({ maxAge: 25, logOnly: false }),
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideStore(),

    provideState('sidebar', sidebarReducer),
    provideState("queue", queueReducer),

    provideEffects(QueueEffects),
  ],
};
