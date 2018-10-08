import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from '../../../Services/authorization.service';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {
  roles = [
    { id: 1, value: 'Doctor' },
    { id: 2, value: 'Patient' },
  ];

  constructor(private service: AuthorizationService,
    public dialogRef: MatDialogRef<SignUpComponent>) { }

  ngOnInit() {
  }

  onClear() {
    this.service.form.reset();
    this.service.initializeFormGroup();
  }

  onClose() {
    this.service.form.reset();
    this.service.initializeFormGroup();
    this.dialogRef.close();
  }

}
