import { Guid } from "guid-typescript";

export interface Notification
{
	id: Guid;
	uptime: number;
	message: string;
}
