import { Component, OnInit, Input } from '@angular/core';
import { Doctor } from 'src/app/Models/Doctors';
import { HOST_URL } from '../../../../config';

const DEFAULT_IMAGE: string = '/assets/img/default-avatar.png';

@Component({
  selector: 'app-doctor-list-item',
  templateUrl: './doctor-list-item.component.html',
  styleUrls: ['./doctor-list-item.component.scss']
})
export class DoctorListItemComponent implements OnInit {
  @Input()
  doctor: Doctor;
  urlAvatar: string;

  constructor() { }

  ngOnInit() {
    this.urlAvatar = this.doctor.avatarURL ? HOST_URL + this.doctor.avatarURL : DEFAULT_IMAGE;
  }

}
