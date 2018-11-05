import { Component, OnInit, ChangeDetectionStrategy, ViewChild, TemplateRef } from '@angular/core';
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { Appointment } from '../Appointment';
import { DoctorplansService } from '../doctorplans.service';

@Component({
  selector: 'app-add-doctor-plans',
  templateUrl: './add-doctor-plans.component.html',
  styleUrls: ['./add-doctor-plans.component.scss']
})
export class AddDoctorPlansComponent implements OnInit {

  constructor(private doctorplansService: DoctorplansService) { }

  ngOnInit() {
  }

  onSubmit() {
    this.doctorplansService.onSubmit();
  }
}
