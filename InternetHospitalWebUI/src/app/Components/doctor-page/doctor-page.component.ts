import { Component, OnInit, HostListener } from '@angular/core';
import { DoctorDetails } from '../../Models/DoctorDetails';
import { DoctorsService } from '../../Services/doctors.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DOCTOR_LIST } from '../../config';

const MIN_WIDTH_FOR_ROW = 900;

@Component({
  selector: 'app-doctor-page',
  templateUrl: './doctor-page.component.html',
  styleUrls: ['./doctor-page.component.scss']
})
export class DoctorPageComponent implements OnInit {

  showLikeRow = true;
  doctor: DoctorDetails;
  constructor(private activateRoute: ActivatedRoute, private service: DoctorsService, private router: Router) {
  }

  ngOnInit() {
    const id = this.activateRoute.snapshot.params['id'];
    this.service.getDoctor(id).subscribe(
      (data: any) => {
        this.doctor = data;
      },
      _ => {
        this.router.navigate([`/${DOCTOR_LIST}`]);
      });
      this.showLikeRow = window.innerWidth > MIN_WIDTH_FOR_ROW;
  }

  @HostListener('window:resize')
  onResize() {
    this.showLikeRow = window.innerWidth > MIN_WIDTH_FOR_ROW;
  }
}
