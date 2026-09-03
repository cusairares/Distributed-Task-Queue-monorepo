import { createAction, props } from "@ngrx/store";
import { QueueData } from "../interfaces/queue-data";

export const loadPage = createAction("[Queue Page] Load Page", props<{ id: string }>())
export const loadPageSucess = createAction("[Queue Page] Load Page Sucess", props<{ data: QueueData }>())
export const loadPageFail = createAction("[Queue Page] Load Page Failure", props<{ error: string }>())