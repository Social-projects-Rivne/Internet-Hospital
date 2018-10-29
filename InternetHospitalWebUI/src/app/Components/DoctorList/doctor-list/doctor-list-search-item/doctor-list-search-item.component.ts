import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Specialization } from 'src/app/Models/Specialization';
import { DoctorsService } from 'src/app/Services/doctors.service';
import { PaginationService } from 'src/app/Services/pagination.service';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-doctor-list-search-item',
  templateUrl: './doctor-list-search-item.component.html',
  styleUrls: ['./doctor-list-search-item.component.scss']
})
export class DoctorListSearchItemComponent implements OnInit {
  @Input()
  specializations: Specialization[];
  @Output()
  onSearch= new EventEmitter<PageEvent>();

  selectedSpecialization = "";
  searchKey: string;  

  constructor(private service:DoctorsService, private paginationService: PaginationService) { }

  ngOnInit() {
  }

  search() {
    this.paginationService.page = 1;
    this.service.httpOptions.params = this.service.httpOptions.params.set("page", this.paginationService.page.toString());
    this.service.getDoctors(this.searchKey,+this.selectedSpecialization);
    let event = new PageEvent();
    event.pageSize = this.paginationService.pageCount;
    event.pageIndex = this.paginationService.page - 1;
    console.log(`Before length ${event.length}`)
    event.length = this.service.doctorsAmount;
    console.log(`After length ${event.length}`)
    this.onSearch.emit(event);
  }

  onSearchClear() {
    this.searchKey = "";
    this.search();
  }
}
