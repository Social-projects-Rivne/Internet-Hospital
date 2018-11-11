import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HOST_URL } from 'src/app/config';

@Injectable({
  providedIn: 'root'
})
export class FeedBackService {

  url = HOST_URL + '/api/feedback/';

  constructor(private http: HttpClient) { }

  form: FormGroup = new FormGroup({
    Text: new FormControl('', Validators.required),
    Type: new FormControl('', Validators.required),
  });

  initializeFormGroup() {
    this.form.setValue({
      Text: '',
      TypeId: 0,
    });
  }
  CreateFeedBack() {
    alert(this.form.value['TypeId']);
    alert(this.form.value['Text']);

    const typeUrl = this.url + 'create';
    return this.http.post(typeUrl, {Text:this.form.value['Text'], TypeId:1});
  }

  getFeedBackTypes(){
    const typeUrl = this.url + 'FeedBackTypes';
    return this.http.get(typeUrl);
  }

}
