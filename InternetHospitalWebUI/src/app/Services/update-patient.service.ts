import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';

import { HOST_URL } from '../config';
import { compareValidator } from '../Directives/compare-validator.directive';
import { API_PATIENT_UPDATE, PATIENT_UPDATE_AVATAR, LOCALE_PHONE } from '../config';
import { MaxDateValidator } from '../Directives/date-validator.directive';

@Injectable({
  providedIn: 'root'
})
export class UpdatePatientService {
  url = HOST_URL + API_PATIENT_UPDATE; 
  avatarUpdateUrl = HOST_URL + PATIENT_UPDATE_AVATAR;

  constructor(private http: HttpClient) { }
  form: FormGroup = new FormGroup({
  PhoneNumber: new FormControl('', Validators.pattern(/\(\d{2}\)\s\d{3}\-\d{2}\-\d{2}/)), 
    FirstName: new FormControl('', Validators.required),
    SecondName: new FormControl('', Validators.required),
    ThirdName: new FormControl('', Validators.required),
    BirthDate: new FormControl('', MaxDateValidator), 
    PassportURL: new FormControl('', Validators.required) 
  });

  initializeFormGroup() {
    this.form.setValue({
      PhoneNumber: '',
      FirstName: '',
      SecondName: '',
      ThirdName: '',
      BirthDate: '',
      PassportURL: '',
    });
  }

  updateAvatar(avatar: File) {
    let formData = new FormData();
    formData.append("Image", avatar);
    return this.http.put(this.avatarUpdateUrl, formData);
  }

  updatePatient(files: FileList) {
    let form = this.form.controls;
    let formData = new FormData();

    for(let i = 0; i < files.length; i++) {
      formData.append("PassportURL", files.item(i));
    }   
    formData.append("PhoneNumber", `${LOCALE_PHONE} ${form.PhoneNumber.value}`);
    formData.append("FirstName", form.FirstName.value);
    formData.append("SecondName", form.SecondName.value);
    formData.append("ThirdName", form.ThirdName.value);
    formData.append("BirthDate", form.BirthDate.value);    

    return this.http.put(this.url, formData);
  }
}
