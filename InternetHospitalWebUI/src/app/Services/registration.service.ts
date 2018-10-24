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

  constructor(private http: HttpClient) { }

  form: FormGroup = new FormGroup({
    Email: new FormControl('', Validators.email),
    Password: new FormControl('', Validators.required),
    ConfirmPassword: new FormControl('', [Validators.required, compareValidator('Password')]),
    Role: new FormControl('', Validators.required),
    Image: new FormControl('', Validators.required)
  });

  file: File;
  
  initializeFormGroup() {
    this.form.setValue({
      Email: '',
      Password: '',
      ConfirmPassword: '',
      Role: '',
      Image: ''
    });
  }

  postUser(user: User, fileToUpload: File) {    
        let formData = new FormData();
        console.log(user.Email);
        console.log(user.Password);
        console.log(user.Role);
        console.log(user.ConfirmPassword);
        
        formData.append("Image", fileToUpload);
        formData.append("Email", user.Email);
        formData.append("Password", user.Password);
        formData.append("ConfirmPassword", user.ConfirmPassword);    
        formData.append("Role", user.Role);    
    
         return this.http.post(this.url, formData);
      }
}
