import { Component, OnInit, HostListener } from '@angular/core';

const MIN_WIDTH_FOR_ROW = 700;

@Component({
  selector: 'app-feedbacks',
  templateUrl: './feedbacks.component.html',
  styleUrls: ['./feedbacks.component.scss']
})
export class FeedbacksComponent implements OnInit {
  startDate = new Date();
  endDate = new Date();
  filterShow = 'row';

  constructor() { }

  ngOnInit() {
    this.filterShow = document.getElementById('filter-form').offsetWidth < MIN_WIDTH_FOR_ROW ? 'column' : 'row';
    const currentMonth = this.startDate.getMonth();
    this.startDate.setMonth(currentMonth - 1);
  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.filterShow = document.getElementById('filter-form').offsetWidth < MIN_WIDTH_FOR_ROW ? 'column' : 'row';
  }
}
