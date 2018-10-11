import { Component, OnInit } from '@angular/core';
import { HOME_IMAGES } from '../../../Mock-Objects/mock-home-news';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  homeImages = HOME_IMAGES;

  constructor() { }

  ngOnInit() {
  }

}
