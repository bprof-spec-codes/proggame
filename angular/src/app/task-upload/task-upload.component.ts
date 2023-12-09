import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-task-upload',
  templateUrl: './task-upload.component.html',
  styleUrls: ['./task-upload.component.scss']
})
export class TaskUploadComponent {
  taskForm: NgForm;
}
