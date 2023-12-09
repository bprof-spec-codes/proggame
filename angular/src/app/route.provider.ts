import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/task-list',
        name: '::Tasks',
        iconClass: 'fas fa-list',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/profile',
        name: '::Account',
        iconClass: 'fas fa-user',
        order: 3,
        layout: eLayoutType.application,
      },
      {
        path: '/shop',
        name: '::Points Shop',
        iconClass: 'fas fa-cart-plus',
        order: 4,
        layout: eLayoutType.application,
      },
      {
        path: '/task-upload',
        name: '::Upload Task',
        iconClass: 'fas fa-plus',
        order: 5,
        layout: eLayoutType.application,
      },
    ]);
  };
}
