import { Component, OnInit, Input } from '@angular/core';
import { Doctor } from 'src/app/Models/Doctors';

@Component({
  selector: 'app-doctor-list-item',
  templateUrl: './doctor-list-item.component.html',
  styleUrls: ['./doctor-list-item.component.scss']
})
export class DoctorListItemComponent implements OnInit {
  @Input()
  doctor: Doctor;
  urlAvatar: string =  '/assets/img/default-avatar.png';

  constructor() { }

  ngOnInit() {
  }

}
