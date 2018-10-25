import { Injectable } from '@angular/core';
import { HOST_URL } from '../config';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Doctor } from '../Models/Doctors';

@Injectable({
  providedIn: 'root'
})
export class DoctorsService {
  doctorsList: Doctor[];
  url = HOST_URL + '/api/Doctors'
  httpOptions = { headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })};

  constructor(private http: HttpClient) { }

  getDoctors() {
    this.http.get<Doctor[]>(this.url, this.httpOptions)
    .subscribe(data => this.doctorsList = data);
  }
}