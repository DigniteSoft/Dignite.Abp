import { Component, OnInit } from '@angular/core';
import { NotificationCenterService } from '../services/notification-center.service';

@Component({
  selector: 'lib-notification-center',
  template: ` <p>notification-center works!</p> `,
  styles: [],
})
export class NotificationCenterComponent implements OnInit {
  constructor(private service: NotificationCenterService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
