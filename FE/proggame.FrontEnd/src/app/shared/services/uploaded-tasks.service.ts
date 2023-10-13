import { Injectable } from '@angular/core';
import { Task } from '../models/task';

@Injectable({
  providedIn: 'root',
})
export class UploadedTasksService {
  files: string[] = [];
  tasks: Task[] = [];
  addFile(file: string) {
    this.files.push(file);
    console.log(file);
    this.tasks.push(this.createFile(file));
    console.log(this.tasks);
  }

  createFile(name: string) {
    let randomTask: Task = {
      id: 0,
      name: name,
      description: `Description for Task`,
      difficulty: 1,
    };
    console.log(randomTask);
    return randomTask;
  }
  getFiles() {
    console.log(this.tasks);
    return this.tasks;
  }

  getFile(num: number) {
    return this.tasks[num];
  }
  getLength() {
    console.log(this.tasks.length);
    return this.tasks.length;
  }
}
