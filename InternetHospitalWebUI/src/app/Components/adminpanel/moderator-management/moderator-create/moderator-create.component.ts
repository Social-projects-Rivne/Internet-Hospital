import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { CreateModeratorService } from '../../Services/create-moderator.service';
import { MODERATORS_MNG } from '../../routesConfig';
import { ADMIN_PANEL } from '../../../../config';

@Component({
  selector: 'app-moderator-create',
  templateUrl: './moderator-create.component.html',
  styleUrls: ['./moderator-create.component.scss']
})
export class ModeratorCreateComponent implements OnInit {

  constructor(private service: CreateModeratorService, private router: Router) {
  }

  ngOnInit() {
  }

  onClear() {
    this.service.form.reset();
  }

  onSubmit(form: NgForm) {
    if (this.service.form.valid) {
      this.service.postModer(form.value).subscribe(res => console.log(res));
      this.router.navigate(['/' + ADMIN_PANEL + '/' + MODERATORS_MNG]);
      this.service.form.reset();
      this.service.initializeFormGroup();
    }
    else {
      this.service.form.reset();
      this.service.initializeFormGroup();
    }
  }

  returnToManagementPage() {
    this.router.navigate(['/' + ADMIN_PANEL + '/' + MODERATORS_MNG]);
  }
}
