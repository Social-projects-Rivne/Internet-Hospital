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
import { HomeComponent } from './Components/Home/home/home.component';
import { HomeNewsComponent } from './Components/Home/home/home-news/home-news.component';
import { HomeNewsItemComponent } from './Components/Home/home/home-news/home-news-item/home-news-item.component';

@NgModule({
  declarations: [
    AppComponent,
    SignUpComponent,
    SignInComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    HomeNewsComponent,
    HomeNewsItemComponent
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
