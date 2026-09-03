import { createFeatureSelector, createSelector } from "@ngrx/store";

export interface SidebarState {
    is_collapsed: Boolean
}

export const selectSidebar = createFeatureSelector<SidebarState>("sidebar");

export const selectSidebarStatus = createSelector(selectSidebar, (state) => state.is_collapsed)