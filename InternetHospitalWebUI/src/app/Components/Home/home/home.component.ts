import { Component, OnInit } from '@angular/core';
import { HomeImages } from '../../../Models/Temp/HomeImage';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  homeImages: HomeImages[] = [
    {id: 1, url: 'https://cdn.vortala.com/static/uploads/8/files/2017/10/midland-park-dental.jpg'},
    {id: 2, url: 'https://cdn.vortala.com/static/uploads/8/files/2018/06/trust-dental-group.jpg'},
    {id: 3, url: 'https://cdn.vortala.com/static/uploads/8/files/2018/06/seaport-dental.jpg'},
    {id: 4, url: 'https://cdn.vortala.com/static/uploads/8/files/2018/06/red-hill-dental.jpg'},
    {id: 5, url: 'https://cdn.vortala.com/static/uploads/8/files/2018/06/focus-orthodontics.jpg'},
    {id: 6, url: 'https://cdn.vortala.com/static/uploads/8/files/2018/06/green-apple-dental.jpg'},
  ]

  constructor() { }

  ngOnInit() {
  }

}
