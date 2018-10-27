import { Component, OnInit, Input } from '@angular/core';
import { DoctorsService } from 'src/app/Services/doctors.service';

@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.scss']
})
export class DoctorListComponent implements OnInit {
  searchKey: string;  

  constructor(private service: DoctorsService) { }

  ngOnInit() {
    this.service.getDoctors();
  }

  onSearchClear() {
    this.searchKey = "";
  }
}