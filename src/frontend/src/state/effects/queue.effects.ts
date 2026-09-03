import { inject, Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, exhaustMap, map, of, switchMap } from "rxjs";
import * as QueueActions from "../actions/queue.actions"
import { DataService } from "../services/dataservice.service";

@Injectable()
export class QueueEffects {

    actions$ = inject(Actions)
    dataService = inject(DataService)

    loadQueue$ = createEffect(() => {
        return this.actions$.pipe(
            ofType(QueueActions.loadPage),
            exhaustMap(({ id }) => this.dataService.fetchQueueData(id).pipe(
                map((data) => QueueActions.loadPageSucess({ data })),
                catchError((error) => of(QueueActions.loadPageFail({ error: error.message })))
            )
            )
        )
    })
}