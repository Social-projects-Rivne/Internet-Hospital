import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { DoctorsService } from 'src/app/Services/doctors.service';
import  { PaginationService } from '../../../Services/pagination.service'
import { PageEvent, MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.scss']
})
export class DoctorListComponent implements OnInit {
  constructor(private service: DoctorsService,private pagService: PaginationService) { }

  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngOnInit() {
    this.service.getDoctors();
    this.service.getSpecializations();
  }
  
  onSearch(event: PageEvent) {
    console.log("Before FP")
    this.paginator.firstPage();
    console.log("After FP")
    console.log("Before Pag Change")
    this.pagService.change(event);
    console.log("After Pag Change")
  }

  pageSwitch(event: PageEvent) { 
    console.log("UNBELIEVABLE!E!@EAS");
    this.pagService.change(event);
    this.service.httpOptions.params = this.service.httpOptions.params.set("page", this.pagService.page.toString());
    this.service.getDoctors();
}
}