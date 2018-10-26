import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './Modules/material/material.module';
import { RoutingModule, ROUTING_COMPONENTS } from './Modules/routing/routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/Layout/header/header.component';
import { FooterComponent } from './Components/Layout/footer/footer.component';
import { HomeNewsComponent } from './Components/Home/home/home-news/home-news.component';
import { HomeNewsItemComponent } from './Components/Home/home/home-news/home-news-item/home-news-item.component';

import { AuthenticationService } from './Services/authentication.service';
import { InterceptorService  } from './Services/interceptor.service';
<<<<<<< HEAD
import { CompareValidatorDirective } from './directives/compare-validator.directive';
import { Page404Component } from './Components/page404/page404.component';
=======
import { CompareValidatorDirective } from './Directives/compare-validator.directive';

import { AuthGuard } from './Services/Guards/auth.guard';
import { PatientGuard } from './Services/Guards/patient.guard';
import { DoctorGuard } from './Services/Guards/doctor.guard';
import { ModeratorGuard } from './Services/Guards/moderator.guard';
import { AdminGuard } from './Services/Guards/admin.guard'
>>>>>>> 422457bb60da0b623afce59d7575fa29fd761aaa

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
  ],
  imports: [
    BrowserModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    RoutingModule,
    HttpClientModule,
  ],
  providers: [AuthenticationService, AuthGuard, PatientGuard, DoctorGuard, ModeratorGuard, AdminGuard, 
    { provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true }],
  bootstrap: [AppComponent],
})
export class AppModule { }


