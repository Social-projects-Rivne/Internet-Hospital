import { Component, OnInit } from '@angular/core';
import { FormGroup, NgForm } from '@angular/forms';
import { FeedBackService } from '../../Services/FeedBackService/feed-back.service';
import { FeedBackType } from '../../Models/FeedBackType';
import { NotificationService } from '../../Services/notification.service'

@Component({
  selector: 'app-feedbacks',
  templateUrl: './feedbacks.component.html',
  styleUrls: ['./feedbacks.component.scss'],
  providers: [FeedBackService]
})
export class FeedbacksComponent implements OnInit {

  feedbackTypes: FeedBackType[];

  constructor(
    private _feedbackService: FeedBackService,
    private _notification: NotificationService
    ) { }

  ngOnInit() {
   this._feedbackService.getFeedBackTypes().subscribe((ftypes: any)  => this.feedbackTypes = ftypes);
  }

  onSubmit() {
    this._feedbackService.CreateFeedBack().subscribe(
        data => {
          this._notification.success(data['message']);
        },
        error => {
          this._notification.error(error);
        });
    this._feedbackService.form.reset();
  }
  onCencel() {
    this._feedbackService.form.reset();
  }

}
