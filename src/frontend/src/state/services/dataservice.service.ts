import { Injectable } from "@angular/core";
import { map, Observable, timer } from "rxjs";
import { QueueData } from "../interfaces/queue-data";

@Injectable({
  providedIn: 'root',
})
export class DataService {
  fetchQueueData(id: string): Observable<QueueData> {
    const mock: QueueData = {};
    return timer(5100).pipe(map(() => mock));
  }
}