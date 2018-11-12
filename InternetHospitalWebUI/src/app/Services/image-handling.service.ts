import { Injectable } from '@angular/core';
import { ImageValidationService } from './image-validation.service';
import { NotificationService } from './notification.service';
import { FormGroup } from '@angular/forms';
import { AuthenticationService } from './authentication.service';

const MIN_HEIGHT: number = 150;
const MAX_HEIGHT: number = 3000;
const MIN_WIDTH: number = 150;
const MAX_WIDTH: number = 3000;

@Injectable({
  providedIn: 'root'
})
export class ImageHandlingService {

  constructor(private validation: ImageValidationService,
    private notification: NotificationService,
    private authentification: AuthenticationService) { }

  defaultImage: string = '../../assets/img/default.png';
  imageUrl: string = this.defaultImage;
  fileToUpload: File = null;
  PassportToUpload: FileList = null;
  isImageValid: boolean = false;
  isPassportUploaded: boolean = false;

  handleFileInput(file: FileList, formGroup?: FormGroup) { // instead of using registrationService just take in this method one more parameter FormGroup
    this.imageUrl = this.defaultImage;
    this.fileToUpload = file.item(0);

    var reader = new FileReader();
    reader.onload = (event: any) => {
      if (event.target.readyState === FileReader.DONE) {
        if (this.validation.isImageFile(event.target.result) == false) {
          this.fileToUpload = null;

          if (localStorage.getItem(this.authentification.token)) {
            let user = JSON.parse(localStorage.getItem(this.authentification.token));
            this.imageUrl = this.authentification.url + user.user_avatar;
          }
          else {
            this.imageUrl = this.defaultImage;
          }

          this.notification.error("Only image file is acceptable!");
          return;
        }
        var img = new Image();
        img.onload = () => {
          if (this.validation.hasImageValidSize(MAX_HEIGHT, MAX_WIDTH, MIN_HEIGHT, MIN_WIDTH, img.height, img.width)) {
            this.imageUrl = event.target.result;
            this.isImageValid = true;
          }
          else {
            if (formGroup != null) {
              formGroup.invalid;  // CHANGED HERE !!!
            }
            this.fileToUpload = null;

            if (localStorage.getItem(this.authentification.token)) {
              let user = JSON.parse(localStorage.getItem(this.authentification.token));
              this.imageUrl = this.authentification.url + user.user_avatar;
            }
            else {
              this.imageUrl = this.defaultImage;
            }

            this.isImageValid = false;
            this.notification.error("Image is invalid! It might be too big or too small.");
          }
        }
        img.src = event.target.result;
      }
    }
    if (this.fileToUpload != null) {
      reader.readAsDataURL(this.fileToUpload);
    }
  }


  handlePassportInput(target, formGroup: FormGroup) {
    this.isPassportUploaded = false;
    this.PassportToUpload = target.files;
    console.log(target.files);
    if (this.PassportToUpload.length > 3 || this.PassportToUpload.length < 3) {
      this.notification.error("You must upload 3 photos (1, 2, 11 pages)");
      target.value = '';
    }
    else {
      for (let i = 0; i < this.PassportToUpload.length; i++) {
        var reader = new FileReader();
        reader.onload = (event: any) => {
          if (event.target.readyState === FileReader.DONE) {
            //  console.log(this.PassportToUpload.item(i).name);
            if (this.validation.isImageFile(event.target.result) == false) {

              console.log("valdation image - false!");
              this.PassportToUpload = null; // at next iteration said that PassportToUpload is null in 86 line
              this.isPassportUploaded = false;
              this.notification.error("Only image file is acceptable!");
              target.value = '';
              return;
            }
            else {
              if (this.PassportToUpload == null) {
                return;
              }
              var img = new Image();
              console.log("before img");
              img.onload = () => {
                console.log("IMG::::  " + img);
                if (this.validation.hasImageValidSize(MAX_HEIGHT, MAX_WIDTH, MIN_HEIGHT, MIN_WIDTH, img.height, img.width)) {
                  this.isPassportUploaded = true;
                  this.notification.success("Images successfully uploaded!")
                }
                else {
                  formGroup.invalid;  // CHANGED HERE !!!
                  this.PassportToUpload = null;
                  this.isPassportUploaded = false;
                  this.notification.error("One or more images are invalid! They might be too big or too small.");
                  target.value = '';
                }
              }
            }
            img.src = event.target.result; // invoke img.onload event

          }
        }
        if (this.PassportToUpload != null) {
          reader.readAsDataURL(this.PassportToUpload.item(i));
        }
      }
    }
  }




}
