export interface Task {
    id: string;
    type?: string;
    status: "COMPLETED" | "PENDING" | "RUNNING" | "FAILED";
    payload: any;
    max_retries: number;
    retries_count: number;
    fencing_token: number;
    created_at?: string;
    execution_duration_ms?: number;
    error_message?: string;
    lease_expires_at?: any;
}