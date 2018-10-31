import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminPanelComponent } from './adminpanel.component';
import { AdminpanelRoutingModule } from './adminpanel-routing.module';

import { SidebarComponent } from './sidebar/sidebar.component';
import { AdminHeaderComponent } from './admin-header/admin-header.component';

import { UserManagementComponent } from '../adminpanel/user-management/user-management.component';
import { RequestManagementComponent } from '../adminpanel/request-management/request-management.component';
import { ModeratorManagementComponent } from '../adminpanel/moderator-management/moderator-management.component';
import { ModeratorCreateComponent } from '../adminpanel/moderator-management/moderator-create/moderator-create.component';
import { ContentManagementComponent } from '../adminpanel/content-management/content-management.component';
import { ContentPreviewComponent } from '../adminpanel/content-management/content-preview/content-preview.component';
import { ContentEditComponent } from '../adminpanel/content-management/content-edit/content-edit.component';
import { ContentItemComponent } from '../adminpanel/content-management/content-item/content-item.component';

import { MatFormFieldModule,
        MatInputModule,
        MatTableModule,
        MatPaginatorModule,
        MatCheckboxModule,
        MatCardModule,
        MatToolbarModule,
        MatIconModule,
        MatListModule,
        MatButtonModule,
        MatMenuModule
      } from '@angular/material';

import { FlexLayoutModule } from '@angular/flex-layout';

import { FormsModule,
        ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  imports: [
    CommonModule,
    AdminpanelRoutingModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatCardModule,
    MatMenuModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    FlexLayoutModule
  ],
  declarations: [
    AdminPanelComponent,
    SidebarComponent,
    AdminHeaderComponent,
    UserManagementComponent,
    RequestManagementComponent,
    ModeratorManagementComponent,
    ModeratorCreateComponent,
    ContentManagementComponent,
    ContentPreviewComponent,
    ContentEditComponent,
    ContentItemComponent
  ],
  providers: []
})
export class AdminpanelModule { }
