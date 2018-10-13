import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { User } from '../Models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  url = 'https://localhost:44390/api/Signup';
  httpOptions = { headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })};  

  constructor(private http: HttpClient) { }

  form: FormGroup = new FormGroup({
    Id: new FormControl(null),
    UserName: new FormControl('', Validators.required),
    Email: new FormControl('', Validators.email),
    Password: new FormControl('', Validators.required),
    ConfirmPassword: new FormControl('', Validators.required),
    Role: new FormControl('Patient'),
    AddressFull: new FormControl('', Validators.required)
  });

  initializeFormGroup() {
    this.form.setValue({
      Id: null,
      UserName: '',
      Email: '',
      Password: '',
      ConfirmPassword: '',
      Role: '',
      AddressFull: '',
    });
  }

  postUser(user: User) {    
    var body = JSON.stringify(user);
    console.log(body);
    return this.http.post<User>(this.url, body, this.httpOptions);
  }  
}
