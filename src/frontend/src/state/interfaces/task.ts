import { Timestamp } from "rxjs";

export interface Task {
    id: string,
    status: "COMPLETED" | "PENDING"
    payload: JSON,
    max_retries: number,
    retries_count: number,
    fencing_token: number,
    lease_expires_at: Timestamp<string>,
}