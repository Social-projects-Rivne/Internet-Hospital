import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from '../../../Services/authorization.service';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { SignUpComponent } from '../../Authorization/sign-up/sign-up.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private authorizationService: AuthorizationService,
    private dialog: MatDialog) { }

  ngOnInit() {
  }

  onSignUp() {
    this.authorizationService.initializeFormGroup();
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.width = "60%";
    this.dialog.open(SignUpComponent, dialogConfig);
  }
}
