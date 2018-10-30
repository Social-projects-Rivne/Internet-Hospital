import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material';
import {TIME_DURATION, HORIZONTAL_ALIGN, VERTICAL_ALIGN, NOTIFICATION_CLASS, SUCCESS_CLASS, ERROR_CLASS} from './../config';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(public snackBar: MatSnackBar) { }

  config: MatSnackBarConfig = {
    duration: TIME_DURATION,
    horizontalPosition: HORIZONTAL_ALIGN,
    verticalPosition: VERTICAL_ALIGN
  }

  error(msg)
  {
    this.config['panelClass'] = [NOTIFICATION_CLASS, ERROR_CLASS];
    this.snackBar.open(msg, '',this.config);
  }

  success(msg) {
    this.config['panelClass'] = [NOTIFICATION_CLASS, SUCCESS_CLASS];
    this.snackBar.open(msg, '',this.config);
  }
}