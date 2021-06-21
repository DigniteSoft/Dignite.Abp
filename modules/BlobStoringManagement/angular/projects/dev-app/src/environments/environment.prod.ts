import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'BlobStoringManagement',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44308',
    redirectUri: baseUrl,
    clientId: 'BlobStoringManagement_App',
    responseType: 'code',
    scope: 'offline_access BlobStoringManagement',
  },
  apis: {
    default: {
      url: 'https://localhost:44308',
      rootNamespace: 'Dignite.Abp.BlobStoringManagement',
    },
    BlobStoringManagement: {
      url: 'https://localhost:44397',
      rootNamespace: 'Dignite.Abp.BlobStoringManagement',
    },
  },
} as Environment;
