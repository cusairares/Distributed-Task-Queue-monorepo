import { createReducer, on } from "@ngrx/store";
import * as SidebarActions from "../actions/sidebar.actions"
import { SidebarState } from "../selectors/sidebar.selectors";

const initialState: SidebarState = {
    is_collapsed: false
}

export const sidebarReducer = createReducer(
    initialState,
    on(SidebarActions.toggleSidebar, (state) => ({
        ...state,
        is_collapsed: !state.is_collapsed
    }))
)