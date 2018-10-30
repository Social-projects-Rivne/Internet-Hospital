import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ModeratorManagementComponent } from './moderator-management/moderator-management.component';
import { RequestManagementComponent } from './request-management/request-management.component';
import { ContentManagementComponent } from './content-management/content-management.component';
import { ModeratorCreateComponent } from './moderator-management/moderator-create/moderator-create.component';
import { UserManagementComponent } from './user-management/user-management.component';
import { AdminPanelComponent } from './adminpanel.component';

import { CONTENTS_MNG, MODERATORS_MNG, REQUESTS_MNG, USERS_MNG, MODER_CREATE} from './routesConfig';
const routes: Routes = [
  {
      path: '',
      component: AdminPanelComponent,
      children: [
          {
              path: '',
              redirectTo: MODERATORS_MNG,
              pathMatch: 'full'
          },
          {
              path: MODERATORS_MNG,
              component: ModeratorManagementComponent,
              pathMatch: 'full'
          },
          {
            path: USERS_MNG,
            component: UserManagementComponent,
            pathMatch: 'full'
          },
          {
              path: REQUESTS_MNG,
              component: RequestManagementComponent,
              pathMatch: 'full'
          },
          {
              path: CONTENTS_MNG,
              component: ContentManagementComponent,
              pathMatch: 'full'
          },
          {
              path: MODER_CREATE,
              component: ModeratorCreateComponent,
              pathMatch: 'full'
          },
      ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminpanelRoutingModule { }
