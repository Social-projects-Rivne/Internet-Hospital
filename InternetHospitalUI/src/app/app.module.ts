import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { MaterialModule } from './Modules/material/material.module';
import { HeaderComponent } from './Components/Layout/header/header.component';
import { SignInComponent } from './Components/Authorization/sign-in/sign-in.component';
import { SignUpComponent } from './Components/Authorization/sign-up/sign-up.component';
import { HomeComponent } from './Components/Home/home/home.component';
import { AuthorizationService } from './Services/authorization.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeNewsComponent } from './Components/Home/home-news/home-news.component';
import { HomeNewsItemComponent } from './Components/Home/home-news-item/home-news-item.component';
import { FooterComponent } from './Components/Layout/footer/footer.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    SignInComponent,
    SignUpComponent,
    HomeComponent,
    HomeNewsComponent,
    HomeNewsItemComponent,
    FooterComponent,
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
