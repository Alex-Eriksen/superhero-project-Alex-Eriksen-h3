import { Component, OnInit } from '@angular/core';
import { Notification } from 'src/app/_models';
import { Guid } from 'guid-typescript';
import { NotificationService } from 'src/app/_services/notification.service';

@Component({
  selector: 'notification-handler',
  templateUrl: './notification-handler.component.html',
  styleUrls: ['./notification-handler.component.css']
})
export class NotificationHandlerComponent implements OnInit
{
	public notificationList: Notification[] = [];

	constructor(private notificationService: NotificationService) { }

	ngOnInit(): void
	{
		this.notificationService.notificationObservable.subscribe(newNotification => this.AddNotification(newNotification));
	}

	public AddNotification( newNotification: Notification ): void
	{
		newNotification.id = Guid.create();
		this.notificationList.push(newNotification);
	}

	public RemoveNotification(notification: Notification): void
	{
		this.notificationList = this.notificationList.filter(n => n.id != notification.id);
	}
}
