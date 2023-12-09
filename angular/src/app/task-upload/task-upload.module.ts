import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TaskUploadRoutingModule } from './task-upload-routing.module';
import { TaskUploadComponent } from './task-upload.component';


@NgModule({
  declarations: [
    TaskUploadComponent
  ],
  imports: [
    CommonModule,
    TaskUploadRoutingModule
  ]
})
export class TaskUploadModule { }
