import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { User } from '../Models/User';
import { HOST_URL } from '../config';
import { compareValidator } from "../Directives/compare-validator.directive";

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  url = HOST_URL + '/api/Signup';
  httpOptions = { headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })};  

  constructor(private http: HttpClient) { }

  form: FormGroup = new FormGroup({
    UserName: new FormControl('', Validators.required),
    Email: new FormControl('', Validators.email),
    Password: new FormControl('', Validators.required),
    ConfirmPassword: new FormControl('', [Validators.required, compareValidator('Password')]),
    Role: new FormControl('', Validators.required),
  });

  initializeFormGroup() {
    this.form.setValue({
      UserName: '',
      Email: '',
      Password: '',
      ConfirmPassword: '',
      Role: '',
    });
  }

  postUser(user: User) {    
    var body = JSON.stringify(user);
    return this.http.post<User>(this.url, body, this.httpOptions);
  }
}
