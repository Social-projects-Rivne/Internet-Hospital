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
    TypeId: new FormControl('', Validators.required),
  });

  
  CreateFeedBack() {
    //alert(this.form.value['TypeId']);
    //alert(this.form.value['Text']);

    const typeUrl = this.url + 'create';
    return this.http.post(typeUrl, {Text:this.form.value['Text'], TypeId:this.form.value['TypeId']});
  }

  getFeedBackTypes(){
    const typeUrl = this.url + 'feedbacktypes';
    return this.http.get(typeUrl);
  }

}
