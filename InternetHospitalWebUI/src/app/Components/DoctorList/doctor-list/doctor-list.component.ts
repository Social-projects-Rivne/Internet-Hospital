import { Component, OnInit } from '@angular/core';
import { DoctorsService } from 'src/app/Services/doctors.service';
import { Observable } from 'rxjs';
import { Doctor } from 'src/app/Models/Doctors';


@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.scss']
})
export class DoctorListComponent implements OnInit {
  //observableData: Observable<Doctor[]>;
  searchKey: string;
  urlAvatar: string =  '/assets/img/default-avatar.png';

  

  constructor(private service: DoctorsService) { }

  ngOnInit() {
    this.service.getDoctors();
    //this.observableData = this.doctors;
    console.log(this.service.doctorsList)
  }

  onSearchClear() {
    this.searchKey = "";
    this.applyFilter();
  }

  applyFilter() {
    //this.doctors.filter = this.searchKey.trim().toLowerCase();
  }
}