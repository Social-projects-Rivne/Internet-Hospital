import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { MaterialModule } from './Modules/material/material.module';
import { HeaderComponent } from './Components/Layout/header/header.component';
import { SignInComponent } from './Components/Authorization/sign-in/sign-in.component';
import { SignUpComponent } from './Components/Authorization/sign-up/sign-up.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    SignInComponent,
    SignUpComponent,
  ],
  imports: [
    BrowserModule,
    MaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
