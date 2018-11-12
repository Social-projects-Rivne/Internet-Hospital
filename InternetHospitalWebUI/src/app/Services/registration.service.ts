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
    Role: new FormControl(''),
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

  postUser(fileToUpload: File) {    
        let formData = new FormData();
        let form = this.form.controls;

        formData.append("Image", fileToUpload);
        formData.append("Email", form.Email.value);
        formData.append("Password", form.Password.value);
        formData.append("ConfirmPassword", form.ConfirmPassword.value);    
        formData.append("Role", form.Role.value);    
    
         return this.http.post(this.url, formData);
      }
}
