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
  onSearch = new EventEmitter();

  selectedSpecialization = "";
  searchKey: string;

  constructor(private service: DoctorsService, private paginationService: PaginationService) { }

  ngOnInit() {
  }

  search() {
    this.onSearch.emit({ searchKey: this.searchKey, selectedSpecialization: this.selectedSpecialization });
  }

  onSearchClear() {
    this.searchKey = "";
  }
}
