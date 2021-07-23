import { Environment } from '@abp/ng.core';

export const environment = {
  production: true,
  remoteEnv: {
    url: 'assets/appsetting.prod.json',
    method: 'GET',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
