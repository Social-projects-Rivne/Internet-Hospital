import { Injectable } from '@angular/core';
import { HomeImages } from '../Models/Temp/HomeImage';
import { HOME_IMAGES } from '../Mock-Objects/mock-home-news';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor() { }

  getHomeImages(): Observable<HomeImages[]> {
    return of(HOME_IMAGES);
  }
}
