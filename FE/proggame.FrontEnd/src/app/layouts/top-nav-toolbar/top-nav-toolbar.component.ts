import { CommonModule } from '@angular/common';
import { Component, NgModule, Input } from '@angular/core';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { DxContextMenuModule, DxListModule } from 'devextreme-angular';
import { IUser } from 'src/app/shared/services';

@Component({
  selector: 'app-top-nav-toolbar',
  templateUrl: './top-nav-toolbar.component.html',
  styleUrls: ['./top-nav-toolbar.component.scss'],
})
export class TopNavToolbarComponent {
  @Input()
  menuItems: any;

  @Input()
  menuMode!: string;

  @Input()
  user!: IUser | null;

  navAvatarIMG: string = './assets/images/nav_avatar.png';

  routeItems: any[] = [
    { text: 'MyHome', link: '/homepage' },
    { text: 'Home', link: '/home' },
    { text: 'Profile', link: '/profile' },
    { text: 'Tasks', link: '/tasks' },
  ];

  routOnClick(text: string) {
    const routeItem = this.routeItems.find((item) => item.text === text);
    if (routeItem) {
      this.router.navigate([routeItem.link]);
    } else {
      this.router.navigate(['**']);
    }
  }

  constructor(private router: Router) {}
}
@NgModule({
  imports: [RouterModule, CommonModule, DxListModule, DxContextMenuModule],
  exports: [TopNavToolbarComponent],
  declarations: [TopNavToolbarComponent],
})
export class TopNavToolbarModule {}
