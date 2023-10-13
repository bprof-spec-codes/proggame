import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomePageComponent } from './components/home-page/home-page.component';
import { TaskListComponent } from './components/task-list/task-list.component';
import { AvatarComponent } from './components/avatar/avatar.component';
import { HomePageRoutingModule } from './home-page.routing.module';
import {
  DxButtonModule,
  DxContextMenuModule,
  DxDataGridModule,
  DxListModule,
} from 'devextreme-angular';
import { ProfileDetailsComponent } from './components/profile-details/profile-details.component';

@NgModule({
  declarations: [
    AvatarComponent,
    HomePageComponent,
    TaskListComponent,
    ProfileDetailsComponent,
  ],
  imports: [
    CommonModule,
    DxListModule,
    DxContextMenuModule,
    DxButtonModule,
    DxDataGridModule,
    HomePageRoutingModule,
  ],
})
export class HomePageModule {}
