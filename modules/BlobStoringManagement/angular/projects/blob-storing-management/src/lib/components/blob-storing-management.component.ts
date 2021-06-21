import { Component, OnInit } from '@angular/core';
import { BlobStoringManagementService } from '../services/blob-storing-management.service';

@Component({
  selector: 'lib-blob-storing-management',
  template: ` <p>blob-storing-management works!</p> `,
  styles: [],
})
export class BlobStoringManagementComponent implements OnInit {
  constructor(private service: BlobStoringManagementService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
