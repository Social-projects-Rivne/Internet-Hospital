import { Component, OnInit, Input } from '@angular/core';
import { HomeImages } from '../../../../../Models/Temp/HomeImage';

@Component({
  selector: 'app-home-news-item',
  templateUrl: './home-news-item.component.html',
  styleUrls: ['./home-news-item.component.scss']
})
export class HomeNewsItemComponent implements OnInit {
  @Input()
  image: HomeImages

  constructor() { }

  ngOnInit() {
  }

}
