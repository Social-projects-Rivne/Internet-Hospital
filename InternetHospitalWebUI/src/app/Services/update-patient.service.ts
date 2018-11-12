import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { HOST_URL } from '../config';
import { compareValidator } from '../Directives/compare-validator.directive';
import { API_PATIENT_UPDATE, PATIENT_UPDATE_AVATAR } from '../config';

@Injectable({
  providedIn: 'root'
})
export class UpdatePatientService {
  url = HOST_URL + API_PATIENT_UPDATE; 
  avatarUpdateUrl = HOST_URL + PATIENT_UPDATE_AVATAR;

  constructor(private http: HttpClient) { }

  form: FormGroup = new FormGroup({
    PhoneNumber: new FormControl('', Validators.pattern(/^\+38\(0\d{2}\)\-\d{3}\-\d{2}\-\d{2}$/)), // add pattern and error msg that shows pattern requirement in <mat-error> in html 
    FirstName: new FormControl('', Validators.required),
    SecondName: new FormControl('', Validators.required),
    ThirdName: new FormControl('', Validators.required),
    BirthDate: new FormControl('', Validators.pattern(/^\d{2}\.\d{2}\.\d{4}/)), // use the same way like for PhoneNumber and also add the same date format handling in Back End
    AvatarURL: new FormControl(''), // check on Back End if avatar is null if NoT null call UploadAvatar()
    PassportURL: new FormControl('', Validators.required) // save in DB like JSON array: ['/passport1.jpg', 'passport2.jpg']
    // Don't forget to set LastStatusChangeTime on BACK END.
    // LastStatusChangeTime what does it mean? status changed or anay user info changed?
  });

  initializeFormGroup() {
    this.form.setValue({
      PhoneNumber: '',
      FirstName: '',
      SecondName: '',
      ThirdName: '',
      BirthDate: '',
      AvatarURL: '',
      PassportURL: '',
    });
  }

  updateAvatar(avatar: File) {
    let formData = new FormData();
    formData.append("Image", avatar);
    return this.http.put(this.avatarUpdateUrl, formData);
  }

  updatePatient(files: FileList/*, newAvatar: File*/) {
    let form = this.form.controls;
    let formData = new FormData();

    for(let i = 0; i < files.length; i++) {
      formData.append("PassportURL", files.item(i));
    }   
    formData.append("PhoneNumber", form.PhoneNumber.value);
    formData.append("FirstName", form.FirstName.value);
    formData.append("SecondName", form.SecondName.value);
    formData.append("BirthDate", form.BirthDate.value);
    // formData.append("AvatarURL", form.AvatarURL.value); // change this, it's File 

    return this.http.put(this.url, formData);
  }

}
