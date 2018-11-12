import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HOST_URL } from 'src/app/config';
import { NotificationService } from '../../Services/notification.service';
import { isUndefined } from 'util';

@Injectable({
  providedIn: 'root'
})
export class FeedBackService {

  url = HOST_URL + '/api/feedback/';

  constructor(private http: HttpClient,
              private _notification: NotificationService
    ) { }

  form: FormGroup = new FormGroup({
    Text: new FormControl('', Validators.required),
    TypeId: new FormControl('', Validators.required),
  });

  CreateFeedBack() {

    const typeUrl = this.url + 'create';
    return this.http.post(typeUrl, {Text: this.form.value['Text'], TypeId: this.form.value['TypeId']});
  }

  getFeedBackTypes() {
    const typeUrl = this.url + 'feedbacktypes';
    return this.http.get(typeUrl);
  }

}
