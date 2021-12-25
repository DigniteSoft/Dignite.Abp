import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'NotificationCenter',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44326',
    redirectUri: baseUrl,
    clientId: 'NotificationCenter_App',
    responseType: 'code',
    scope: 'offline_access NotificationCenter role email openid profile',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44326',
      rootNamespace: 'Dignite.Abp.NotificationCenter',
    },
    NotificationCenter: {
      url: 'https://localhost:44320',
      rootNamespace: 'Dignite.Abp.NotificationCenter',
    },
  },
} as Environment;
