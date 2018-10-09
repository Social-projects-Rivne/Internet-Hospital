import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { SignUpComponent } from './Components/Authorization/sign-up/sign-up.component';
import { SignInComponent } from './Components/Authorization/sign-in/sign-in.component';
import { HeaderComponent } from './Components/Layout/header/header.component';
import { FooterComponent } from './Components/Layout/footer/footer.component';
import { MaterialModule } from './Modules/material/material.module';
import { AuthorizationService } from './Services/authorization.service';

@NgModule({
  declarations: [
    AppComponent,
    SignUpComponent,
    SignInComponent,
    HeaderComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule, 
  ],
  providers: [AuthorizationService],
  bootstrap: [AppComponent],
  entryComponents: [SignUpComponent]
})
export class AppModule { }
