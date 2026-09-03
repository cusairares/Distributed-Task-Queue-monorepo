import { createFeatureSelector, createSelector } from "@ngrx/store";
import { Queue } from "../interfaces/queue";



export const selectQueue = createFeatureSelector<Queue>("queue");

export const selectData = createSelector(selectQueue, (state) => (state.data))

export const selectLoadingStatus = createSelector(selectQueue, (state) => (state.loading))

export const selectError = createSelector(selectQueue, (state) => (state.error))




