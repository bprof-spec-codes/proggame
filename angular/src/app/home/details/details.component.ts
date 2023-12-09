import { Component } from '@angular/core';
import { Profile } from 'src/app/shared/models/profile';
import { UploadedTasksService } from 'src/app/shared/services/uploaded-tasks.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss'],
})
export class DetailsComponent {
  profileDetails: Profile;
  constructor(private uploadedTasksService: UploadedTasksService) {
    this.profileDetails = {
      Firstname: 'Test',
      Lastname: 'User',
      Username: 'testuser',
      Email: 'testuser@test.ts',
      Description: 'Lorem ipsum test.',
      TotalBadges: 99,
      TotalTasks: uploadedTasksService.getLength(),
    };
  }

  profileProperties: { label: string; value: string | number }[] = [];

  onRowPrepared(e: any) {
    e.rowElement.style.backgroundColor = '#161920eb';
  }

  ngOnInit() {
    for (const key in this.profileDetails) {
      if (this.profileDetails.hasOwnProperty(key)) {
        this.profileProperties.push({
          label: key,
          value: (this.profileDetails as any)[key],
        });
      }
    }
  }
}
