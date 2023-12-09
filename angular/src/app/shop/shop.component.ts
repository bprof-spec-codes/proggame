import { Component, OnInit } from '@angular/core';

interface Badge {
  id: number;
  name: string;
  imagePath: string;
  price: number;
}

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  badges: Badge[] = [];

  ngOnInit(): void {
    this.populateBadges();
  }

  populateBadges(): void {
    const badgeCount = 25; // Total number of badges
    const badgePrice = 100; // Example price for each badge

    for (let i = 1; i <= badgeCount; i++) {
      this.badges.push({
        id: i,
        name: `Badge ${i}`,
        imagePath: `../../assets/images/badges/badge${i}.jpg`, // Adjust the path as necessary
        price: badgePrice // This can be dynamic if prices vary
      });
    }
  }

  purchaseBadge(badgeId: number): void {
    // Implement the purchase logic here
    console.log(`Badge ${badgeId} purchased!`);
  }
}
