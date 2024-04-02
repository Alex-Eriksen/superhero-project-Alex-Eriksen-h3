import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Notification } from 'src/app/_models';

@Component({
  	selector: 'notification',
  	templateUrl: './notification.component.html',
	styleUrls: [ './notification.component.css' ]
})
export class NotificationComponent implements OnInit
{
	@Input() data: Notification = { id: Guid.createEmpty(), uptime: 0, message: '' };
	@Output() removeSelf: EventEmitter<Notification> = new EventEmitter();
	animOut: boolean = false;

  	constructor() { }
	ngOnInit(): void
	{
		let animationTime = this.data.uptime - (this.data.uptime / 5);

		setTimeout(() =>
		{
			this.animOut = true;
		}, animationTime);

		setTimeout(() =>
		{
			this.removeSelf.emit(this.data);
		}, this.data.uptime);
	}
}
