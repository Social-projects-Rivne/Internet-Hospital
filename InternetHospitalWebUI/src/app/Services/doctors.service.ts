import { Injectable } from '@angular/core';
import { HOST_URL } from '../config';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Doctor } from '../Models/Doctors';
import { PaginationService } from './pagination.service';
import { Specialization } from '../Models/Specialization';

@Injectable({
  providedIn: 'root'
})
export class DoctorsService {
  doctorsList: Doctor[];
  specializations: Specialization[];
  doctorsAmount: number;

  url = HOST_URL + '/api/Doctors';
  
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
    params: new HttpParams()
      .set("page", this.paginationService.pageIndex.toString())
      .set("pagecount", this.paginationService.pageSize.toString())
  };

  constructor(private http: HttpClient,
    private paginationService: PaginationService) { }

  getDoctors(name?: string, specialization?: number) {
    let doctorsUrl = this.url;
    if (name != null && name != "") {
      this.httpOptions.params =  this.httpOptions.params.set("searchbyname", name);
    }
    else {
      this.httpOptions.params = this.httpOptions.params.delete("searchbyname");
    }

    if (specialization != 0 && !isNaN(specialization)) {
      this.httpOptions.params =  this.httpOptions.params.set("searchbyspecialization", specialization.toString())
    }
    else {
      this.httpOptions.params = this.httpOptions.params.delete("searchbyspecialization");
    }

    this.http.get(doctorsUrl, this.httpOptions)
    .subscribe((result: any) => {
      this.doctorsList=result.doctors;
      this.doctorsAmount = result.totalDoctors;
  });
  }

  getSpecializations() {
    let specUrl = this.url + "/specializations";
    this.http.get<Specialization[]>(specUrl)
      .subscribe(data => this.specializations = data);
  }
}