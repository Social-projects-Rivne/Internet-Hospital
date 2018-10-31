import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './Modules/material/material.module';
import { RoutingModule, ROUTING_COMPONENTS } from './Modules/routing/routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';

import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/Layout/header/header.component';
import { FooterComponent } from './Components/Layout/footer/footer.component';
import { HomeNewsComponent } from './Components/Home/home/home-news/home-news.component';
import { HomeNewsItemComponent } from './Components/Home/home/home-news/home-news-item/home-news-item.component';
import { Page404Component } from './Components/page404/page404.component';
import { DoctorListComponent } from './Components/DoctorList/doctor-list/doctor-list.component';
import { DoctorListItemComponent } from './Components/DoctorList/doctor-list/doctor-list-item/doctor-list-item.component';

import { AuthenticationService } from './Services/authentication.service';
import { InterceptorService  } from './Services/interceptor.service';

import { CompareValidatorDirective } from './Directives/compare-validator.directive';

import { AuthGuard } from './Services/Guards/auth.guard';
import { PatientGuard } from './Services/Guards/patient.guard';
import { DoctorGuard } from './Services/Guards/doctor.guard';
import { ModeratorGuard } from './Services/Guards/moderator.guard';
import { AdminGuard } from './Services/Guards/admin.guard';
import { DoctorListSearchItemComponent } from './Components/DoctorList/doctor-list/doctor-list-search-item/doctor-list-search-item.component'

import { AdminpanelModule } from './Components/adminpanel/adminpanel.module';

@NgModule({
  declarations: [
    AppComponent,
    ROUTING_COMPONENTS, 
    HeaderComponent,
    FooterComponent,
    HomeNewsComponent,
    HomeNewsItemComponent,
    CompareValidatorDirective,
    Page404Component,
    DoctorListComponent,
    DoctorListItemComponent,
    DoctorListSearchItemComponent
  ],
  imports: [
    BrowserModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    RoutingModule,
    HttpClientModule,
    FlexLayoutModule,
    AdminpanelModule
  ],
  exports:[MaterialModule],
  providers: [AuthenticationService, AuthGuard, PatientGuard, DoctorGuard, ModeratorGuard, AdminGuard, 
    { provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true }],
  bootstrap: [AppComponent],
})
export class AppModule { }


