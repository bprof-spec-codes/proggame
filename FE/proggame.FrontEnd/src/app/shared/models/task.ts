import { TaskListComponent } from 'src/app/pages/home-page/components/task-list/task-list.component';
import { Difficulty } from '../enums/difficulty';

export class Task {
  id!: number;
  name!: string;
  description!: string;
  difficulty!: Difficulty;
}
