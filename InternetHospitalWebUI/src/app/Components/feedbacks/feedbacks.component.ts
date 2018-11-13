import { Component, OnInit } from '@angular/core';
import { FormGroup, NgForm } from '@angular/forms';
import { FeedBackService } from '../../Services/FeedBackService/feed-back.service';
import { FeedBackType } from '../../Models/FeedBackType';
import { NotificationService } from '../../Services/notification.service';
import { Router } from '@angular/router';
import { HOST_URL } from 'src/app/config';

@Component({
  selector: 'app-systemfeedbacks',
  templateUrl: './feedbacks.component.html',
  styleUrls: ['./feedbacks.component.scss'],
  providers: [FeedBackService]
})
export class FeedbacksComponent implements OnInit {

  feedbackTypes: FeedBackType[];

  constructor(
    private _feedbackService: FeedBackService,
    private _notification: NotificationService,
    private _router: Router
    ) { }

  ngOnInit() {
   this._feedbackService.getFeedBackTypes().subscribe((ftypes: any)  => this.feedbackTypes = ftypes);
  }

  onSubmit() {
    this._feedbackService.CreateFeedBack().subscribe(
        data => {
          this._notification.success('Thanks for your feedback');
          this._router.navigate(['/']);
        },
        error => {
          this._notification.error(error);
        });
    this._feedbackService.form.reset();
  }
  onCancel() {
    this._feedbackService.form.reset();
  }

}
