import { Environment } from '@abp/ng.core';

export const environment = {
  production: false,
  remoteEnv: {
    url: 'assets/appsetting.dev.json',
    method: 'GET',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
