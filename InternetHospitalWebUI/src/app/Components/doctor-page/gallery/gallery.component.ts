import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '@angular/material';
import { ImageModalDialogComponent } from './image-modal-dialog.component';
import { Overlay } from '@angular/cdk/overlay';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss']
})

export class GalleryComponent implements OnInit {

  @Input() images: string[];
  startIndex = 0;
  widthOf1Image = 134;

  constructor(private dialog: MatDialog, private overlay: Overlay) { }

  ngOnInit() {
  }

  next() {
    if (Math.abs(--this.startIndex) === this.images.length - 4) {
      this.startIndex = 0;
    }
    console.log(this.startIndex);
  }

  previous() {
    ++this.startIndex;
    if (this.startIndex > 0) {
      this.startIndex = -this.images.length + 5;
    }
    console.log(this.startIndex);
  }

  openDialog(i) {
    const strategy = this.overlay.scrollStrategies.block();
    const data = new ImageData();
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

export class ImageData {
  images: string[];
  selected: number;
}
