import { Component, OnInit, HostListener } from '@angular/core';

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
    this.filterShow = document.getElementById('filter-form').offsetWidth < 700 ? 'column' : 'row';
    const currentMonth = this.startDate.getMonth();
    this.startDate.setMonth(currentMonth - 1);
  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.filterShow = document.getElementById('filter-form').offsetWidth < 700 ? 'column' : 'row';
    console.log(this.filterShow);
  }
}
