import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'proggame',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44320/',
    redirectUri: baseUrl,
    clientId: 'proggame_App',
    responseType: 'code',
    scope: 'offline_access proggame',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44320',
      rootNamespace: 'proggame',
    },
  },
} as Environment;
