import { Component, Input, HostListener, AfterContentChecked } from '@angular/core';
import { MatDialog } from '@angular/material';
import { ImageModalDialogComponent } from './image-modal-dialog.component';
import { Overlay } from '@angular/cdk/overlay';
import { GalleryModel } from '../../../Models/GalleryModel';

const SUM_OF_WIDTH_OF_SWITCH_BUTTONS = 80;

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss']
})

export class GalleryComponent implements AfterContentChecked {

  @Input() images: string[];
  startIndex = 0;
  widthOf1Image = 134;
  amountOfPicsOnScreen = 1;
  width: number;

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    const currWidth = document.getElementById('gallery-container').offsetWidth;
    this.amountOfPicsOnScreen = Math.trunc((currWidth - SUM_OF_WIDTH_OF_SWITCH_BUTTONS)
                                              / this.widthOf1Image);

  }

  constructor(private dialog: MatDialog, private overlay: Overlay) { }

  ngAfterContentChecked() {
    const currWidth = document.getElementById('gallery-container').offsetWidth;
    this.amountOfPicsOnScreen = Math.trunc((currWidth - SUM_OF_WIDTH_OF_SWITCH_BUTTONS)
                                              / this.widthOf1Image);
  }

  next() {
    if (Math.abs(--this.startIndex) === this.images.length - this.amountOfPicsOnScreen + 1) {
      this.startIndex = 0;
    }
  }

  previous() {
    ++this.startIndex;
    if (this.startIndex > 0) {
      this.startIndex = -this.images.length + this.amountOfPicsOnScreen;
    }
  }

  openDialog(i) {
    const strategy = this.overlay.scrollStrategies.block();
    const data = new GalleryModel();
    data.images = this.images;
    data.selected = i;
    this.dialog.open(ImageModalDialogComponent, {
      panelClass: 'custom-dialog-container',
      scrollStrategy: strategy,
      autoFocus: true,
      data: data
    });
  }
}
