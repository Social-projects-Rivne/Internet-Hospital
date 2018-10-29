import { Component, OnInit, Input } from '@angular/core';
import { HomeImages } from '../../../../Models/Temp/HomeImage';
import { HomeService } from 'src/app/Services/home.service';

@Component({
  selector: 'app-home-news',
  templateUrl: './home-news.component.html',
  styleUrls: ['./home-news.component.scss']
})
export class HomeNewsComponent implements OnInit {
  @Input()
  homeImages: HomeImages[];

  constructor(private homeService: HomeService) { }

  ngOnInit() {
    this.getHomeImages();
  }

  getHomeImages(): void {
    this.homeService.getHomeImages()
        .subscribe(homeImages => this.homeImages = homeImages);
  }
}
