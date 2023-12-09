import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskUploadComponent } from './task-upload.component';

const routes: Routes = [{ path: '', component: TaskUploadComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TaskUploadRoutingModule { }
