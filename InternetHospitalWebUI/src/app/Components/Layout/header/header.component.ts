import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../Services/authentication.service';
import { Observable} from 'rxjs';
import { UsersProfileService } from '../../../Services/users-profile.service';
import { HOST_URL } from '../../../config';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService, private usersProfileService: UsersProfileService) { }

  isLoggedIn: Observable<boolean>;
  isPatient: Observable<boolean>;
  isDoctor: Observable<boolean>;
  isModerator: Observable<boolean>;
  isAdmin: Observable<boolean>;
  userAvatar: string;

  ngOnInit() {
    this.isLoggedIn = this.authenticationService.isLoggedIn();
    this.isPatient = this.authenticationService.isPatient();
    this.isDoctor = this.authenticationService.isDoctor();
    this.isModerator = this.authenticationService.isModerator();
    this.isAdmin = this.authenticationService.isAdmin();
    this.usersProfileService.getImage()
      .subscribe((data: any) => { this.userAvatar = HOST_URL + data.avatarURL; });
  }
}
