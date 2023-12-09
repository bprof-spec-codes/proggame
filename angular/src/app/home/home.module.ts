import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { AvatarComponent } from './avatar/avatar.component';
import { DetailsComponent } from './details/details.component';
import { TaskListComponent } from './task-list/task-list.component';
import { DxButtonModule, DxContextMenuModule, DxDataGridModule, DxListModule } from 'devextreme-angular';

@NgModule({
  declarations: [HomeComponent, AvatarComponent, DetailsComponent, TaskListComponent],
  imports: [
    SharedModule, 
    HomeRoutingModule, 
    DxListModule,
    DxContextMenuModule,
    DxButtonModule,
    DxDataGridModule,],
})
export class HomeModule {}
