import { Component, OnInit, Input } from '@angular/core';
import { HomeImages } from '../../../../Models/Temp/HomeImage';

@Component({
  selector: 'app-home-news',
  templateUrl: './home-news.component.html',
  styleUrls: ['./home-news.component.scss']
})
export class HomeNewsComponent implements OnInit {
  @Input()
  homeImages: HomeImages[];

  constructor() { }

  ngOnInit() {
  }

}
