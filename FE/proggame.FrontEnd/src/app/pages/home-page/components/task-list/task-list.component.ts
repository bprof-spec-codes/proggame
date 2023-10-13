import { Component } from '@angular/core';
import { Task } from 'src/app/shared/models/task';
import { UploadedTasksService } from 'src/app/shared/services/uploaded-tasks.service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss'],
})
export class TaskListComponent {
  tasks: Task[] = [];

  constructor(private uploadtaskservice: UploadedTasksService) {}

  ngOnInit() {
    this.tasks = this.uploadtaskservice.getFiles();
    console.log(this.tasks);
  }
}
