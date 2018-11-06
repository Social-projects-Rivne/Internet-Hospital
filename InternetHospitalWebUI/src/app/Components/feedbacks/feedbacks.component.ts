import { Component, OnInit } from '@angular/core';
import { FormGroup, NgForm } from '@angular/forms';
import { FeedBackService } from '../../Services/FeedBackService/feed-back.service';
import { NotificationService } from '../../Services/notification.service';
import { FeedBackType } from '../../Models/FeedBackType';

@Component({
  selector: 'feedbacks',
  templateUrl: './feedbacks.component.html',
  styleUrls: ['./feedbacks.component.scss'],
  providers:[FeedBackService]
})
export class FeedbacksComponent implements OnInit {

  feedbackTypes:FeedBackType[];

  constructor(
    private _feedbackService:FeedBackService,
    private _notification: NotificationService
    ) { }

  ngOnInit() {

   this._feedbackService.form.controls['Type'].setValue('FeedBack');
   this._feedbackService.getFeedBackTypes().subscribe((ftypes:any)  => this.feedbackTypes = ftypes);
  }

  onSubmit(){
    this._feedbackService.postUser();
    this._feedbackService.form.reset();
  }

}
