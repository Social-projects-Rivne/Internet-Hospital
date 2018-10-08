import { Component } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { AuthorizationService } from './Services/authorization.service';
import { SignUpComponent } from './Components/Authorization/sign-up/sign-up.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private authorizationService: AuthorizationService,
    private dialog: MatDialog) { }


  onSignUp() {
    this.authorizationService.initializeFormGroup();
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.width = "60%";
    this.dialog.open(SignUpComponent, dialogConfig);
  }
}
