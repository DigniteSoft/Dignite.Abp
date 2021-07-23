import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'Abp',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44398',
    redirectUri: baseUrl,
    clientId: 'Abp_App',
    responseType: 'code',
    scope: 'offline_access Abp role email openid profile',
  },
  apis: {
    default: {
      url: 'https://localhost:44398',
      rootNamespace: 'Dignite.Abp',
    },
    Abp: {
      url: 'https://localhost:44343',
      rootNamespace: 'Dignite.Abp',
    },
  },
} as Environment;
