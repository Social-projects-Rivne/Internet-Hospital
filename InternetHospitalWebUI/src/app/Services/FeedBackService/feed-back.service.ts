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
  postUser() {
    let formData = new FormData();
    
    formData.append("Type", this.form.value['Type']);
    formData.append("Email", this.form.value['Type']);
    //add current user id

    return this.http.post(this.url, formData);
  }

  getFeedBackTypes(){
    const typesURL = this.url + 'api/FeedBackTypes';
    return this.http.get(typesURL);
  }

}
