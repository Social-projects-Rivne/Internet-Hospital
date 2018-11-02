import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Specialization } from 'src/app/Models/Specialization';
import { Filter } from "../../../../Models/Filter";

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

  filter = new Filter();

  constructor() { }

  ngOnInit() {
  }

  search() {
    this.onSearch.emit(this.filter);
  }

  onSearchClear() {
    this.filter.searchKey = "";
  }
}
