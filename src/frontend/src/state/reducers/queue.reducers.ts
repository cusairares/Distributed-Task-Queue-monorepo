import { createReducer, on } from "@ngrx/store";
import * as QueueActions from "../actions/queue.actions"
import { Queue } from "../interfaces/queue";

const initialState: Queue = { data: null, loading: true, error: null }

export const queueReducer = createReducer(
    initialState,
    on(QueueActions.loadPage, (state) => ({
        ...state,
        loading: true
    })),
    on(QueueActions.loadPageSucess, (state, { data }) => ({
        ...state,
        data,
        loading: false
    })),
    on(QueueActions.loadPageFail, (state, { error }) => ({
        ...state,
        error,
        loading: false,
    }))
)