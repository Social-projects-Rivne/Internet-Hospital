import { Injectable } from '@angular/core';
import { FeedBack } from '../../Models/FeedBack';

@Injectable({
  providedIn: 'root'
})
export class FeedBackService {

  CurrentFeedBack: FeedBack ;

  constructor() { }
}
