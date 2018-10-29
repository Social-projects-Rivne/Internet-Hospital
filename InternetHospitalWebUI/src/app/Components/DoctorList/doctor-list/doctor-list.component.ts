import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { DoctorsService } from 'src/app/Services/doctors.service';
import { PaginationService } from '../../../Services/pagination.service'
import { PageEvent, MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.scss']
})
export class DoctorListComponent implements OnInit {
  constructor(private service: DoctorsService, private pagService: PaginationService) { }

  private isWithParams: Boolean;
  private searchKey: string;
  private selectedSpecialization: string;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngOnInit() {
    this.service.getDoctors();
    this.service.getSpecializations();
  }

  onSearch($event) {
    this.searchKey = $event.searchKey;
    this.selectedSpecialization = $event.selectedSpecialization;
    if (this.searchKey || this.selectedSpecialization) {
      this.isWithParams = true;
    }
    else {
      this.isWithParams = false;
    }
    this.paginator.firstPage();
    let event = new PageEvent();
    event.pageSize = this.pagService.pageSize;
    event.pageIndex = this.pagService.pageIndex - 1;
    event.length = this.service.doctorsAmount;
    this.pageSwitch(event);
  }

  pageSwitch(event: PageEvent) {
    this.pagService.change(event);
    this.service.httpOptions.params = this.service.httpOptions.params.set("page", this.pagService.pageIndex.toString());
    if (this.isWithParams == true) {
      this.service.getDoctors(this.searchKey, +this.selectedSpecialization);
    }
    else {
      this.service.getDoctors();
    }
  }
}