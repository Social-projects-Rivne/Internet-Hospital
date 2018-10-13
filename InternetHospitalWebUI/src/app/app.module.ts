import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './Modules/material/material.module';
import { RoutingModule, routingComponents } from './Modules/routing/routing.module';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/Layout/header/header.component';
import { FooterComponent } from './Components/Layout/footer/footer.component';
import { HomeNewsComponent } from './Components/Home/home/home-news/home-news.component';
import { HomeNewsItemComponent } from './Components/Home/home/home-news/home-news-item/home-news-item.component';

@NgModule({
  declarations: [
    AppComponent,
    routingComponents,
    HeaderComponent,
    FooterComponent,
    HomeNewsComponent,
    HomeNewsItemComponent
  ],
  imports: [
    BrowserModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    RoutingModule,
    HttpClientModule 
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }


