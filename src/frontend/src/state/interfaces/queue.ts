import { QueueData } from "./queue-data";

export interface Queue {
    data: QueueData | null,
    loading: boolean,
    error: string | null,
}