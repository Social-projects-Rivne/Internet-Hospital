import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { RegistrationService } from '../../../Services/registration.service';
import {MatGridListModule} from '@angular/material/grid-list';
import { ImageValidationService } from '../../../Services/image-validation.service';
import { NotificationService } from '../../../Services/notification.service';
import { first } from 'rxjs/operators';
import { SIGN_IN } from './../../../config'

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
     private notification: NotificationService
    ) { }

  defaultImage: string = '../../../assets/img/default.png';
  imageUrl: string = this.defaultImage;
  fileToUpload: File = null;
  isImageValid: boolean = false;
  ngOnInit() {
    this.service.form.controls['Role'].setValue('Patient');
  }

  onClear() {
    this.service.form.reset();
  }

  handleFileInput(file : FileList){
	this.imageUrl = this.defaultImage;
    this.fileToUpload = file.item(0);

    var reader = new FileReader();
    reader.onload = (event: any) => {
      if(event.target.readyState === FileReader.DONE)
      {
        if (this.validation.isImageFile(event.target.result) == false )
        {
          this.fileToUpload = null;
          this.imageUrl = this.defaultImage;
          this.notification.error("Only image file is acceptable!");
          return;
        }
        var img = new Image();
        img.onload = () =>
        {
            if(this.validation.hasImageValidSize(MAX_HEIGHT, MAX_WIDTH, MIN_HEIGHT, MIN_WIDTH, img.height, img.width))
            {
              this.imageUrl = event.target.result;
              this.isImageValid = true; 
            }
            else
            {
                this.service.form.invalid;
                this.fileToUpload = null;
                this.imageUrl = this.defaultImage;
                this.isImageValid = false;
                this.notification.error("Image is invalid! It might be too big or too small.");
            }
        }
		img.src = event.target.result;
      }
    }
	if (this.fileToUpload != null) 
	{
		reader.readAsDataURL(this.fileToUpload);
	}
  }

  onSubmit(form: NgForm) {    
    this.service.postUser(form.value, this.fileToUpload)
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