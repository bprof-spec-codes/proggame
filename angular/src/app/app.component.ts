import { eThemeLeptonXComponents } from '@abp/ng.theme.lepton-x';
import { Component } from '@angular/core';
import { FooterComponent, HeaderComponent, UserPanelComponent } from './shared/components';
import { ReplaceableComponentsService } from '@abp/ng.core';
import { UserMenu } from '@abp/ng.theme.shared';
import { LoginComponent } from '@abp/ng.account';

@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
  `,
})
export class AppComponent {
  constructor(
    private replaceableComponents: ReplaceableComponentsService, // injected the service
  ) {
  this.replaceableComponents.add({
    component: HeaderComponent,
    key: eThemeLeptonXComponents.Toolbar,
  });
  this.replaceableComponents.add({
    component: FooterComponent,
    key: eThemeLeptonXComponents.Footer,
  })
  }

}
