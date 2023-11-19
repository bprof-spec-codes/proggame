import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  get(): import("../models/task").Task[] {
    throw new Error('Method not implemented.');
  }

  constructor() { }
}
