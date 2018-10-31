import { Component, OnInit } from '@angular/core';
import { CONTENTS_MNG, MODERATORS_MNG, REQUESTS_MNG, USERS_MNG } from '../routesConfig';
import { ADMIN_PANEL } from '../../../config';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  constructor() { }

  users = "/admpanel/" + this.users;

  ngOnInit() {
    console.log(this.users);
   }
}
