import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class NotificationCenterService {
  apiName = 'NotificationCenter';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/NotificationCenter/sample' },
      { apiName: this.apiName }
    );
  }
}
