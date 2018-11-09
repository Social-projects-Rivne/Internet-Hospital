import { Component, OnInit, Input } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-feedback-item',
  templateUrl: './feedback-item.component.html',
  styleUrls: ['./feedback-item.component.scss'],
  animations: [
    trigger('bodyExpand', [
      state('collapsed', style({ height: '20px' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('150ms cubic-bezier(0.74, 0.97, 0.91, 1)')),
    ]),
  ],
})
export class FeedbackItemComponent implements OnInit {
  isExpanded = false;
  tempText = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Provident sed sequi ex ipsam.  Cumque voluptate debitis laboriosam quia, officia repellendus quisquam quasi nisi voluptatum voluptatem, accusamus doloremque, ad facere. Alias optio cumque maxime corporis repudiandae adipisci iure, quod aut hic molestiae sapiente qui blanditiis delectus perspiciatis sunt"

  @Input() isPositive: Boolean;
  constructor() { }

  ngOnInit() {
    console.log(this.isPositive);
  }
}
