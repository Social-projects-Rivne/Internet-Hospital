import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

import { AuthorizationService } from '../../../Services/authorization.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {

  constructor(private service: AuthorizationService) { }

  ngOnInit() {
  }

  onClear() {
    this.service.form.reset();
    this.service.initializeFormGroup();
  }

  onSubmit(form: NgForm) {
    if(this.service.form.valid) {
      this.service.postUser(form.value).subscribe(res => console.log(res));
      this.service.form.reset();
      this.service.initializeFormGroup();
    }
  }
}
