import { Injectable } from '@angular/core';
import { HOST_URL } from 'src/app/config';
import { first } from 'rxjs/operators';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { NotificationService } from 'src/app/Services/notification.service';
import { CalendarEvent } from 'calendar-utils';
import { Appointment } from './Appointment';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { CalendarEventAction, CalendarEventTimesChangedEvent } from 'angular-calendar';
import { COLORS } from 'src/app/Mock-Objects/mock-colors';
import { Subject } from 'rxjs';
import { startOfDay, endOfDay, isSameMonth, isSameDay } from 'date-fns';

@Injectable({
  providedIn: 'root'
})
export class DoctorplansService {
  events: CalendarEvent[] = [];
  Appointments: Appointment[] = [];
  refresh: Subject<any> = new Subject();
  activeDayIsOpen: boolean = false;
  viewDate: Date = new Date();
  loginForm: FormGroup = this.formBuilder.group({
    start: ['', Validators.required],
    end: ['', Validators.required]
  });

  actions: CalendarEventAction[] = [
    {
      label: '<i class="fa fa-fw fa-pencil-alt"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        alert("edit " + event.title);
        //this.handleEvent('Edited', event);
      }
    },
    {
      label: '<i class="fa fa-fw fa-times"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        // this.events = this.events.filter(iEvent => iEvent !== event);
        // this.handleEvent('Deleted', event);
      }
    }
  ];

  constructor(private http: HttpClient,
    private notification: NotificationService,
    private formBuilder: FormBuilder, ) { }

  onSubmit() {
    let specUrl = HOST_URL + "/api/Appointment/Add";

    console.log(this.loginForm.controls["start"].value);
    console.log(this.loginForm.controls["end"].value);
    this.http.post(specUrl, { starttime: this.loginForm.controls["start"].value, endtime: this.loginForm.controls["end"].value })
      .pipe(first())
      .subscribe((data: any) => {
        this.notification.success(data["message"]);
        this.getAppointments();
      },
        error => {
          this.notification.error(error);
        }
      );
  }

  getAppointments() {
    this.events = [];
    let specUrl = HOST_URL + "/api/Appointment/GetAppointments";
    this.http.get<Appointment[]>(specUrl)
      .subscribe((data: any) => {
        this.Appointments = data.appointments;
        this.Map();
      }
      );
  }

  Map() {
    let color;
    let title;
    this.Appointments.forEach(element => {
      if (element.statusId === 1)
        color = COLORS.red;
      else
        color = COLORS.blue;
      title = "Empty";
      this.events.push({
        title: title,
        start: new Date(element.startTime),
        end: new Date(element.endTime),
        color: color,
        actions: this.actions
      });

    });
    this.refresh.next();
  }

  eventTimesChanged({ event, newStart, newEnd } : CalendarEventTimesChangedEvent) : void {
    event.start = newStart;
    event.end = newEnd;
    this.handleEvent('Dropped or resized', event);
    this.refresh.next();
  }

  handleEvent(action: string, event: CalendarEvent): void {
    console.log(action + event.title);
    // this.modalData = { event, action };
    // this.modal.open(this.modalContent, { size: 'lg' });
  }

  addEvent(): void {
    this.events.push({
      title: 'New event',
      start: startOfDay(new Date()),
      end: endOfDay(new Date()),
      color: COLORS.red,
      draggable: true,
      resizable: {
        beforeStart: true,
        afterEnd: true
      }
    });
    this.refresh.next();
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (this.activeDayIsOpen = true)
        this.activeDayIsOpen = false;
      this.viewDate = date;
      if ((isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) || events.length === 0) {
        this.activeDayIsOpen = false;
      }
      else {
        this.activeDayIsOpen = true;
      }
    }
  }
}
