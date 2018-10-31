import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Content } from '../../../../Models/Content';

const SLIDE_TIME_IN_MSC = 3500;

@Component({
  selector: 'app-content-item',
  templateUrl: './content-item.component.html',
  styleUrls: ['./content-item.component.scss'],
  animations: [
    trigger('imageDisplay', [
      state('displayed', style({opacity: '1'})),
      state('hidden', style({opacity: '0'})),
      transition('displayed <=> hidden', animate('500ms cubic-bezier(0.74, 0.97, 0.91, 1)')),
    ]),
  ],
})

export class ContentItemComponent implements OnInit {

  constructor() {
  }

  slideIndex = 0;
  @Input() content: Content;

  @Output() onChanged = new EventEmitter<Content>();
  @Output() onDeleted = new EventEmitter<Content>();

  imgs = [ 'https://whitehousepawprints.com/wp-content/uploads/2017/05/family-2.jpg',
  'https://www.maritimefirstnewspaper.com/wp-content/uploads/2018/07/family-3.jpg',
  'https://vanierinstitute.ca/wp-content/uploads/2016/05/Diversity-diversit%C3%A9.jpg'
];

  ngOnInit() {
    setInterval(() => {
      this.nextImg();
    }, SLIDE_TIME_IN_MSC);
  }

  delete() {
    this.onDeleted.emit(this.content);
  }

  nextImg() {
    if (this.slideIndex < this.imgs.length - 1) {
      this.slideIndex++;
    } else {
      this.slideIndex = 0;
    }
  }

  prevImg() {
    if (this.slideIndex !== 0) {
      this.slideIndex--;
    } else {
      this.slideIndex = this.imgs.length - 1;
    }
  }

  edit() {
    this.onChanged.emit(this.content);
  }
}
