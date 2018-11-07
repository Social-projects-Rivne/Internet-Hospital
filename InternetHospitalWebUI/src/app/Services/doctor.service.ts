import { Injectable } from '@angular/core';
import { HOST_URL, API_DOCTORS } from '../config';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { DoctorDetails } from '../Models/doctor-details';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  constructor(private http: HttpClient) { }

  private url = HOST_URL + API_DOCTORS;

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  getDoctor(id: number) {
    const doctorsUrl = `${this.url}/${id}`;
    return this.http.get(doctorsUrl, this.httpOptions);
  }

}
