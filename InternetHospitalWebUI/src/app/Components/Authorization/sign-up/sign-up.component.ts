import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { RegistrationService } from '../../../Services/registration.service';
import {MatGridListModule} from '@angular/material/grid-list';
import { ImageValidationService } from '../../../Services/image-validation.service';


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

  constructor(private service: RegistrationService, private validation: ImageValidationService, private router: Router) { }

  defaultImage: string = '../../../assets/img/default-image.png';
  imageUrl: string = this.defaultImage;
  fileToUpload: File = null;
  isImageValid: boolean = false;
  ngOnInit() {
  }

  onClear() {
    this.service.form.reset();
  }

  handleFileInput(file : FileList){
    this.fileToUpload = file.item(0);

    var reader = new FileReader();
    reader.onload = (event: any) => {
      if(event.target.readyState === FileReader.DONE)
      {
        var img = new Image();
        img.src = event.target.result;
        img.onload = () =>
        {
            if(this.validation.hasImageValidSize(MAX_HEIGHT, MIN_WIDTH, MIN_HEIGHT, MIN_WIDTH, img.height, img.width))
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

              alert("Image is invalid! It might be too big or too small.");
            }
        }
      }
    }
    reader.readAsDataURL(this.fileToUpload);
  }

onSubmit(form: NgForm) {
    if(this.service.form.valid) {
      this.service.postUser(form.value, this.fileToUpload).subscribe(res => console.log(res));      
      this.router.navigate(['/sign-in']);
      this.service.form.reset();
      this.service.initializeFormGroup();
    }
    else
    {
      this.service.form.reset();
      this.service.initializeFormGroup();
    }
  }
}
