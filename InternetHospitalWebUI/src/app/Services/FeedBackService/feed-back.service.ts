import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HOST_URL } from 'src/app/config';

@Injectable({
  providedIn: 'root'
})
export class FeedBackService {

  url = HOST_URL + '/api/FeedBack';

  constructor(private http: HttpClient) { }

  form: FormGroup = new FormGroup({
    Text: new FormControl('', Validators.required),
    Type: new FormControl('', Validators.required),
  });

  initializeFormGroup() {
    this.form.setValue({
      Text: '',
      Type: '',
    });
  }
  CreateFeedBack() {
    let formData = new FormData();
    
    formData.append("Type", this.form.value['Type']);
    formData.append("Text", this.form.value['Text']);

    return this.http.post(this.url, formData);
  }

  getFeedBackTypes(){
    const typesURL = this.url + 'api/FeedBackTypes';
    return this.http.get(typesURL);
  }

}
