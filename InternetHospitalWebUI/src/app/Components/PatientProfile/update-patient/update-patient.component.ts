import { Component, OnInit } from '@angular/core';
import { NgForm, FormGroup } from '@angular/forms';
import { UpdatePatientService } from '../../../Services/update-patient.service';
import { NotificationService } from '../../../Services/notification.service';
import { ImageHandlingService } from '../../../Services/image-handling.service';
import { LOCALE_PHONE } from '../../../config';

@Component({
  selector: 'app-update-patient',
  templateUrl: './update-patient.component.html',
  styleUrls: ['./update-patient.component.scss']
})
export class UpdatePatientComponent implements OnInit {

  locale = LOCALE_PHONE;
  
  constructor(private service: UpdatePatientService, 
    private notification: NotificationService,
    private imageHandling: ImageHandlingService,
  ) { }

  ngOnInit() {
  }

  onClear() {
    this.service.form.reset();
  }

  onSubmit(form: NgForm) {
      this.service.updatePatient(this.imageHandling.PassportToUpload)
        .subscribe(
            data => {      
              this.notification.success(data["message"]);
              this.service.form.reset();
              this.service.initializeFormGroup();               
            },
            error => {
              this.notification.error(error);
            });
  }
}
