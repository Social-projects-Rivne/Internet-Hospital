import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { RegistrationService } from '../../../Services/registration.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {

  constructor(private service: RegistrationService, private router: Router) { }

  ngOnInit() {
  }

  onClear() {
    this.service.form.reset();
  }

  onSubmit(form: NgForm) {
    if(this.service.form.valid) {
      this.service.postUser(form.value).subscribe();
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
