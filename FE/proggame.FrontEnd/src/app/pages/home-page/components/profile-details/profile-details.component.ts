import { Component } from '@angular/core';
import { Profile } from 'src/app/shared/models/profile';

@Component({
  selector: 'app-profile-details',
  templateUrl: './profile-details.component.html',
  styleUrls: ['./profile-details.component.scss'],
})
export class ProfileDetailsComponent {
  profileDetails: any = {
    Firstname: 'Test',
    Lastname: 'User',
    Username: 'testuser',
    Email: 'testuser@test.ts',
    Description: 'Lorem ipsum test.',
    TotalBadges: 99,
    TotalTasks: 69,
  };

  profileProperties: { label: string; value: string | number }[] = [];

  ngOnInit() {
    for (const key in this.profileDetails) {
      if (this.profileDetails.hasOwnProperty(key)) {
        this.profileProperties.push({
          label: key,
          value: this.profileDetails[key].toString(),
        });
      }
    }
  }
}
