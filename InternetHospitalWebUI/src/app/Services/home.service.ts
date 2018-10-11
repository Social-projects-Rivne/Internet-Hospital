import { Injectable } from '@angular/core';
import { HomeImages } from '../Models/Temp/HomeImage';
import { HOME_IMAGES } from '../Mock-Objects/mock-home-news';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor() { }

  getHomeImages(): HomeImages[] {
    return HOME_IMAGES;
  }
}
