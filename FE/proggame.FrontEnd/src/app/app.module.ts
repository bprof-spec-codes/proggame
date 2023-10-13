import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {
  SideNavOuterToolbarModule,
  SideNavInnerToolbarModule,
  SingleCardModule,
} from './layouts';
import {
  FooterModule,
  ResetPasswordFormModule,
  CreateAccountFormModule,
  ChangePasswordFormModule,
  LoginFormModule,
} from './shared/components';
import { AuthService, ScreenService, AppInfoService } from './shared/services';
import { UnauthenticatedContentModule } from './unauthenticated-content';
import { AppRoutingModule } from './app-routing.module';
import {
  TopNavToolbarComponent,
  TopNavToolbarModule,
} from './layouts/top-nav-toolbar/top-nav-toolbar.component';
import { RegisterComponent } from './shared/components/register/register.component';
import { LogoutComponent } from './shared/components/logout/logout.component';
import { LoginComponent } from './shared/components/login/login.component';
import { TaskListComponent } from './shared/components/task-list/task-list.component';
import { UploadComponent } from './shared/components/upload/upload.component';

@NgModule({
  declarations: [
    AppComponent,
    TaskListComponent,
    UploadComponent,
    LoginComponent,
    LogoutComponent,
    RegisterComponent,
  ],
  imports: [
    BrowserModule,
    SideNavOuterToolbarModule,
    SideNavInnerToolbarModule,
    TopNavToolbarModule,
    SingleCardModule,
    FooterModule,
    ResetPasswordFormModule,
    CreateAccountFormModule,
    ChangePasswordFormModule,
    LoginFormModule,
    UnauthenticatedContentModule,
    AppRoutingModule,
  ],
  providers: [AuthService, ScreenService, AppInfoService],
  bootstrap: [AppComponent],
  exports: [
    TaskListComponent
  ]
})
export class AppModule {}
