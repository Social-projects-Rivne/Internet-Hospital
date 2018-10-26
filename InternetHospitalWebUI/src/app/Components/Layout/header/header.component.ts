import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../../Services/authentication.service';
import { Observable} from 'rxjs';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService) { }

  isLoggedIn : Observable<boolean>;
  isPatient : Observable<boolean>;
  isDoctor : Observable<boolean>;
  isModerator : Observable<boolean>;
  isAdmin: Observable<boolean>;

  ngOnInit() {
    this.isLoggedIn = this.authenticationService.isLoggedIn();
    this.isPatient = this.authenticationService.isPatient();
    this.isDoctor = this.authenticationService.isDoctor();
    this.isModerator = this.authenticationService.isModerator();
    this.isAdmin = this.authenticationService.isAdmin();
  }   
}
