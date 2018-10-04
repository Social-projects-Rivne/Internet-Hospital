import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import {MatToolbarModule} from '@angular/material/toolbar';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    MatToolbarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
