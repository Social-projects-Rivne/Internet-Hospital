import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-feedback-item',
  templateUrl: './feedback-item.component.html',
  styleUrls: ['./feedback-item.component.scss']
})
export class FeedbackItemComponent implements OnInit {
  tempText = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Provident sed sequi ex ipsam.  Cumque voluptate debitis laboriosam quia, officia repellendus quisquam quasi nisi voluptatum voluptatem, accusamus doloremque, ad facere. Alias optio cumque maxime corporis repudiandae adipisci iure, quod aut hic molestiae sapiente qui blanditiis delectus perspiciatis sunt"

  @Input() isPositive: Boolean;
  constructor() { }

  ngOnInit() {
  }
}
