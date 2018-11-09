import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { RegistrationService } from '../../../Services/registration.service';
import {MatGridListModule} from '@angular/material/grid-list';
import { ImageValidationService } from '../../../Services/image-validation.service';
import { NotificationService } from '../../../Services/notification.service';
import { first } from 'rxjs/operators';
import { SIGN_IN } from './../../../config'
import { ImageHandlingService } from '../../../Services/image-handling.service';

const MIN_HEIGHT: number = 150;
const MAX_HEIGHT: number = 3000;
const MIN_WIDTH: number = 150;
const MAX_WIDTH: number = 3000;

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {

  constructor(private service: RegistrationService,
     private validation: ImageValidationService,
     private router: Router,
     private notification: NotificationService,
     private imageHandling: ImageHandlingService 
    ) { }

  ngOnInit() {
    this.service.form.controls['Role'].setValue('Patient');
  }

  onClear() {
    this.service.form.reset();
  }

  onSubmit(form: NgForm) {    
    this.service.postUser(form.value, this.imageHandling.fileToUpload)
        .pipe(first())
        .subscribe(
            data => {      
              this.router.navigate([SIGN_IN]);
              this.notification.success(data["message"]);
              this.service.form.reset();
              this.service.initializeFormGroup();               
            },
            error => {
              this.notification.error(error);
            });
  }
}