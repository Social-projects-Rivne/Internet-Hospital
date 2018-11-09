import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { ImageData } from './gallery.component';

@Component({
    selector: 'app-image-modal-dialog',
    templateUrl: 'image-modal-dialog.component.html',
    styleUrls: ['image-modal-dialog.component.scss']
  })
export class ImageModalDialogComponent {
  constructor( public dialogRef: MatDialogRef<ImageModalDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ImageData) {
      console.log({data});
    }

    change(dIndex) {
      this.data.selected += dIndex;
      if (this.data.selected < 0) {
        this.data.selected = this.data.images.length - 1;
      }
      else if (this.data.selected === this.data.images.length) {
        this.data.selected = 0;
      }
    }
}
