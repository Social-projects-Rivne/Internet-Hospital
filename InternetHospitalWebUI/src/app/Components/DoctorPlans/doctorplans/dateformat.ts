import { CalendarDateFormatter, DateFormatterParams } from 'angular-calendar';
import { LOCALE_ID, Inject } from '@angular/core';
import { CalendarEventTitleFormatter, CalendarEvent } from 'angular-calendar';
import { DatePipe } from '@angular/common';


export class CustomDateFormatter extends CalendarDateFormatter {
  
    public weekViewHour({ date, locale }: DateFormatterParams): string {
        return new Intl.DateTimeFormat('ca', {
            hour: 'numeric',
            minute: 'numeric'
          }).format(date);
    }
  }

  export class CustomEventTitleFormatter extends CalendarEventTitleFormatter {
    constructor(@Inject(LOCALE_ID) private locale: string) {
      super();
    }
  
    // you can override any of the methods defined in the parent class
  
    month(event: CalendarEvent): string {
      return `<b>${new DatePipe(this.locale).transform(
        event.start,
        'HH:mm',
        this.locale
      )}</b> ${event.title}`;
    }
  
    week(event: CalendarEvent): string {
      return `<b>${new DatePipe(this.locale).transform(
        event.start,
        'HH:mm',
        this.locale
      )}</b> ${event.title}`;
    }
}