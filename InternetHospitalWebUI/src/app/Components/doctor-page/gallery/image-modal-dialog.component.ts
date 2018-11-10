import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { GalleryModel } from '../../../Models/GalleryModel';

@Component({
  selector: 'app-image-modal-dialog',
  templateUrl: 'image-modal-dialog.component.html',
  styleUrls: ['image-modal-dialog.component.scss']
})
export class ImageModalDialogComponent {
  constructor(public dialogRef: MatDialogRef<ImageModalDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: GalleryModel) {
  }

  change(dIndex) {
    this.data.selected += dIndex;
    if (this.data.selected < 0) {
      this.data.selected = this.data.images.length - 1;
    } else if (this.data.selected === this.data.images.length) {
      this.data.selected = 0;
    }
  }
}
