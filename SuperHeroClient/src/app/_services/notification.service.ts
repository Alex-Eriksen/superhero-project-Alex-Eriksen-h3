import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable, Subject } from 'rxjs';
import { Notification } from '../_models';

@Injectable({
  providedIn: 'root'
})
export class NotificationService
{
	private notificationSubject: Subject<Notification>;
	public notificationObservable: Observable<Notification>;

	constructor()
	{
		this.notificationSubject = new Subject<Notification>();
		this.notificationObservable = this.notificationSubject.asObservable();
	}

	public AddNotification( uptime: number, message: string ): void
	{
		this.notificationSubject.next({ id: Guid.createEmpty(), uptime: uptime, message: message });
	}
}
