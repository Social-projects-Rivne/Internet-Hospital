import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl } from '@angular/forms';

import { Moder } from '../../../Models/Moder';
import { HOST_URL } from '../../../config';
import { compareValidator } from '../../../Directives/compare-validator.directive';

@Injectable({
  providedIn: 'root'
})
export class EditModeratorService {

  url = HOST_URL + '/somelink';
  httpOptions = { headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })};

  constructor(private http: HttpClient) { }

  form: FormGroup = new FormGroup({
    Name: new FormControl(''),
    Surname: new FormControl(''),
    Lastname: new FormControl(''),
    Password: new FormControl(''),
    ConfirmPassword: new FormControl('', [compareValidator('Password')]),
  });

  initializeFormGroup() {
    this.form.setValue({
      Name: '',
      Surname: '',
      Lastname: '',
      Password: '',
      ConfirmPassword: '',
    });
  }

  putModer(moder: Moder) {
    const body = JSON.stringify(moder);
    return this.http.put<Moder>(this.url, body, this.httpOptions);
  }
}

