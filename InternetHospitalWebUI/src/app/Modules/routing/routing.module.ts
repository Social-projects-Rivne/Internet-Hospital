import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

import { SignUpComponent } from '../../Components/Authorization/sign-up/sign-up.component';
import { SignInComponent } from '../../Components/Authorization/sign-in/sign-in.component';
import { HomeComponent } from '../../Components/Home/home/home.component';
import { Page404Component } from '../../Components/page404/page404.component';
import { DoctorListComponent } from '../../Components/DoctorList/doctor-list/doctor-list.component';
import { FeedbacksComponent } from '../../Components/feedbacks/feedbacks.component';

import { AuthGuard } from '../../Services/Guards/auth.guard';
import { PatientGuard } from '../../Services/Guards/patient.guard';
import { DoctorGuard } from '../../Services/Guards/doctor.guard';
import { ModeratorGuard } from '../../Services/Guards/moderator.guard';
import { AdminGuard } from '../../Services/Guards/admin.guard';

import { ADMIN_PANEL, DOCTOR_LIST, PAGE_404, MY_PLANS, FEEDBACKS } from '../../config';
import { SIGN_IN } from '../../config';
import { SIGN_UP } from '../../config';
import { HomeNewsComponent } from 'src/app/Components/Home/home/home-news/home-news.component';
import { DoctorPlansComponent } from 'src/app/Components/DoctorPlans/doctorplans/doctorplans.component';

const ROUTES: Routes = [
  {
    path: '', component: HomeComponent, children: [
      { path: '', component: HomeNewsComponent },
      { path: SIGN_UP, component: SignUpComponent },
      { path: SIGN_IN, component: SignInComponent },
      { path: DOCTOR_LIST, component: DoctorListComponent },
      { path: MY_PLANS, component: DoctorPlansComponent, canActivate: [DoctorGuard] },
      { path: FEEDBACKS, component: FeedbacksComponent },
    ]
  },
  { path: PAGE_404, component: Page404Component },
  { path: ADMIN_PANEL, redirectTo: ADMIN_PANEL, canActivate: [AdminGuard]},
  { path: '**', redirectTo: PAGE_404 },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(ROUTES)
  ],
  declarations: [],
  exports: [RouterModule]
})
export class RoutingModule { }

export const ROUTING_COMPONENTS = [
  SignUpComponent,
  SignInComponent,
  HomeComponent,
  Page404Component,
  DoctorListComponent,
  HomeNewsComponent,
  DoctorPlansComponent
];
