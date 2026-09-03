import { inject, Injectable, signal } from "@angular/core";
import { Store } from "@ngrx/store";
import { toggleSidebar } from "./actions/sidebar.actions";
import { selectSidebarStatus } from "./selectors/sidebar.selectors";
import * as QueueActions from "./actions/queue.actions"
import { selectError, selectLoadingStatus } from "./selectors/queue.selectors";
@Injectable({
    providedIn: "root"
})
export class QueueFacade {

    store = inject(Store);

    //why do we use select signal and not a store selector
    isSidebarOpen$ = this.store.selectSignal(selectSidebarStatus);
    loading$ = this.store.selectSignal(selectLoadingStatus)
    error$ = this.store.selectSignal(selectError)

    handleSidebarToggle() {
        this.store.dispatch(toggleSidebar());
    }

    loadData(id: string): void {
        this.store.dispatch(QueueActions.loadPage({ id }));
    }
}