import { Component, OnInit } from '@angular/core';
import { HomeService } from '../../../Services/home.service';
import { HomeImages } from '../../../Models/Temp/HomeImage';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  homeImages: HomeImages[];

  constructor(private homeService: HomeService) { }

  ngOnInit() {
    this.getHomeImages();
  }

  getHomeImages(): void {
    this.homeImages = this.homeService.getHomeImages();
  }
}
