import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HOST_URL, PATIENT_GET_AVATAR, PATIENT_UPDATE_AVATAR } from '../config';

@Injectable({
    providedIn: 'root'
 })
export class UsersProfileService {

    url = HOST_URL + PATIENT_GET_AVATAR;

    constructor(private http: HttpClient) {}

    getImage() {
        return this.http.get(this.url);
    }

    updateAvatar(fileAvatar: File = null) {
        const formData = new FormData();
        formData.append('Image', fileAvatar);
        return this.http.put(HOST_URL + PATIENT_UPDATE_AVATAR, formData).subscribe();
      }

}
