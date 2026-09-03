import { Task } from "./task";

export interface QueueData {
    name?: string,
    state?: "RUN" | "PAUSE",
    tasks?: Array<Task>
    size?: number,
    memory_usage?: number,
    processed?: number,
    failed?: number,
    error_rate?: number,
}
