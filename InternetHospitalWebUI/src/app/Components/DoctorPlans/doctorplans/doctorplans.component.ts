import { Component, OnInit, ChangeDetectionStrategy, ViewChild, TemplateRef } from '@angular/core';
import { CalendarEvent, CalendarView, CalendarEventAction } from 'angular-calendar';
import { Appointment } from '../Appointment';
import { DoctorplansService } from '../doctorplans.service';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { Subject } from 'rxjs';
import { isSameMonth, isSameDay } from 'date-fns';
import { NotificationService } from 'src/app/Services/notification.service';
import { COLORS } from 'src/app/Mock-Objects/mock-colors';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

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
  refresh: Subject<any> = new Subject();  
  activeDayIsOpen: boolean = false;

  
  
  loginForm: FormGroup;

  constructor(private doctorplansService: DoctorplansService,
              private dialog: MatDialog,
              private notification: NotificationService,
              private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.getAppointments();

    this.loginForm = this.formBuilder.group({
      start: ['', Validators.required],
      end: ['', Validators.required]
  });
  }

  getAppointments() {
    this.doctorplansService.getAppointments()
    .subscribe((data: any) => {
      this.Appointments = data.appointments;
      this.Map();
      this.refresh.next();
    }); 
  }

  handleEvent(action: string, event: CalendarEvent): void {
    console.log(action + event.title);
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (this.activeDayIsOpen = true)
        this.activeDayIsOpen = false;
      this.viewDate = date;
      if ((isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) || events.length === 0) {
        this.activeDayIsOpen = false;
      }else {
        this.activeDayIsOpen = true;
      }
    }
  }

  private Map() {
    let color;
    let title;    
    this.Appointments.forEach(element => {
    let actions: CalendarEventAction[]=[];
      console.log(element.status);
      if (element.status === "Reserved"){  
        title = "Reserved by";      
        actions.push(this.getUserAction(element.id,element.userFirstName, element.userSecondName));
        actions.push(this.cancelAction);
        color = COLORS.yellow;;
      }
      else{
        title = "Empty";
        actions.push(this.deleteAction);
        color = COLORS.green;
      }        
      
      this.events.push({
        id:element.id,
        title: title,
        start: new Date(element.startTime),
        end: new Date(element.endTime),
        color: color,
        actions: actions
      });
    });
  }

  deleteAction: CalendarEventAction={
    label: '<i></i>',
    onClick: ({ event }: { event: CalendarEvent }): void => {
      if(confirm("Are you sure ?")) {
        this.doctorplansService.deleteAppointment(event.id)
          .subscribe((data: any) => {
            this.getAppointments();
            this.notification.success(data["message"]);
        },
          error => {
          this.getAppointments();
          this.notification.error(error);
        });;    
      }     
    },
    cssClass:"fas fa-trash-alt text-danger"
  }

  cancelAction: CalendarEventAction={
    label: '<i></i>',
    onClick: ({ event }: { event: CalendarEvent }): void => {
      if(confirm("Are you sure ?")) {
          this.doctorplansService.cancelAppointment(event.id)
          .subscribe((data: any) => {
            this.getAppointments();
          this.notification.success(data["message"]);
        },
        error => {
          this.getAppointments();
          this.notification.error(error);
        });;    
      }      
    },
    cssClass:"far fa-times-circle fa-lg text-danger"
  }

  getUserAction(id:number,name:string,secondname:string):CalendarEventAction{
    return {
        label: '<i>'+name+' '+secondname+'</i>  ',
        onClick: ({ event }: { event: CalendarEvent }): void => {
          alert("ЗАХОЖУ НА ЮЗЕРА "+name+" "+secondname);           
      },
      cssClass:"text-success"
    }
  }

  onSubmit() {
    console.log(this.loginForm.controls["start"].value);
    console.log(this.loginForm.controls["end"].value);
    this.doctorplansService.addAppointment(this.loginForm.controls["start"].value, this.loginForm.controls["end"].value)
    .subscribe((data: any) => {
       this.notification.success(data["message"]);
  },
  error => {
    this.notification.error(error);
  });; ;
  }
}