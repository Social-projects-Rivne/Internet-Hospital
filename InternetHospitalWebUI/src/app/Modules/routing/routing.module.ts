import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

import { SignUpComponent } from '../../Components/Authorization/sign-up/sign-up.component';
import { SignInComponent } from '../../Components/Authorization/sign-in/sign-in.component';
import { HomeComponent } from '../../Components/Home/home/home.component';
import { Page404Component } from '../../Components/page404/page404.component'

const ROUTES: Routes = [
  { path: '', component: HomeComponent },
  { path: 'sign-up', component: SignUpComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: '404', component: Page404Component },

  { path: '**', redirectTo: '404' },
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
]
