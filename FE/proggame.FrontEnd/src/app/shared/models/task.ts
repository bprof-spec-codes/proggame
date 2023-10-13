import { Difficulty } from '../enums/difficulty';

export class Task {
  id!: number;
  name!: string;
  description!: string;
  difficulty!: Difficulty;
}
