import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FeedBackService } from '../../Services/FeedBackService/feed-back.service';

@Component({
  selector: 'feedbacks',
  templateUrl: './feedbacks.component.html',
  styleUrls: ['./feedbacks.component.scss'],
  providers:[FeedBackService]
})
export class FeedbacksComponent implements OnInit {

  constructor(private _FeedBackService:FeedBackService) { }

  ngOnInit() {
  }

}
