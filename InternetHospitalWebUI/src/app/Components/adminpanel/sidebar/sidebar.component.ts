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

  users = '/'  + ADMIN_PANEL +  '/' + USERS_MNG;
  moders = '/'  + ADMIN_PANEL +  '/' + MODERATORS_MNG;
  contents = '/' + ADMIN_PANEL + '/' + CONTENTS_MNG;
  requests = '/' + ADMIN_PANEL + '/' + REQUESTS_MNG;

  ngOnInit() {
   }
}
