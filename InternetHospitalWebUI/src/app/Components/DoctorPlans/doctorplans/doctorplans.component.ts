import { Component, OnInit, ChangeDetectionStrategy, ViewChild, TemplateRef } from '@angular/core';
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { Appointment } from '../Appointment';
import { DoctorplansService } from '../doctorplans.service';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { AddDoctorPlansComponent } from '../add-doctor-plans/add-doctor-plans.component';

@Component({
  selector: 'app-doctorplans',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './doctorplans.component.html',
  styleUrls: ['./doctorplans.component.scss']
})
export class DoctorPlansComponent implements OnInit {
  @ViewChild('modalContent')
  modalContent: TemplateRef<any>;
  view: CalendarView = CalendarView.Month;
  CalendarView = CalendarView;
  viewDate: Date = new Date();
  Appointments: Appointment[] = [];
  events: CalendarEvent[] = [];

  modalData: {
    action: string;
    event: CalendarEvent;
  };

  constructor(private doctorplansService: DoctorplansService,
              private dialog: MatDialog,) { }

  ngOnInit() {
    //this.getAppointments();
  }

  getAppointments() {
    this.doctorplansService.getAppointments();
  }

  onCreate() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "80%";
    this.dialog.open(AddDoctorPlansComponent, dialogConfig);
  }
}