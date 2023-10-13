import { Component } from '@angular/core';
import { Task } from 'src/app/shared/models/task';

@Component({
  templateUrl: 'home.component.html',
  styleUrls: [ './home.component.scss' ]
})

export class HomeComponent {

  tasks: Task[] = [];

  constructor() {
    for (let i = 1; i <= 5; i++) {
      let randomTask: Task = {
        id: i,
        name: `Task ${i}`,
        description: `Description for Task ${i}`,
        difficulty: Math.floor(Math.random() * 3) +1 // Random difficulty level: 0 for Easy, 1 for Medium, 2 for Hard
      };
      this.tasks.push(randomTask);
    }
    console.log(this.tasks)
  }

  ngOnInit(){
  }


}
