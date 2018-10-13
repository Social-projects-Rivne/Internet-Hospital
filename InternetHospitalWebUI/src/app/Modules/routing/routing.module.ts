import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

import { SignUpComponent } from '../../Components/Authorization/sign-up/sign-up.component';
import { SignInComponent } from '../../Components/Authorization/sign-in/sign-in.component';
import { HomeComponent } from '../../Components/Home/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'sign-up', component: SignUpComponent },
  { path: 'sign-in', component: SignInComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  declarations: [],
  exports: [RouterModule]
})
export class RoutingModule { }

export const routingComponents = [
  SignUpComponent,
  SignInComponent,
  HomeComponent,
]
