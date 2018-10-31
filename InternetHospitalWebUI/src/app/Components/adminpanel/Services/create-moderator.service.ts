import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { Moder } from '../../../Models/Moder';
import { HOST_URL } from '../../../config';
import { compareValidator } from '../../../Directives/compare-validator.directive';


@Injectable({
  providedIn: 'root'
})
export class CreateModeratorService {

  url = HOST_URL + '/api/Signup';
  httpOptions = { headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })};

  constructor(private http: HttpClient) { }

  form: FormGroup = new FormGroup({
    Email: new FormControl('', Validators.email),
    Name: new FormControl('', Validators.required),
    Surname: new FormControl('', Validators.required),
    Lastname: new FormControl('', Validators.required),
    Password: new FormControl('', Validators.required),
    ConfirmPassword: new FormControl('', [Validators.required, compareValidator('Password')]),
  });

  initializeFormGroup() {
    this.form.setValue({
      Email: '',
      Password: '',
      Name: '',
      Surname: '',
      Lastname: '',
      ConfirmPassword: '',
    });
  }

  postModer(moder: Moder) {
    const body = JSON.stringify(moder);
    return this.http.post<Moder>(this.url, body, this.httpOptions);
  }
}
