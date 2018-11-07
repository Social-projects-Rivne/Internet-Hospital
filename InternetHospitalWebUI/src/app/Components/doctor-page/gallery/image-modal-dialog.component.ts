import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
    selector: 'app-image-modal-dialog',
    templateUrl: 'image-modal-dialog.component.html',
  })
export class ImageModalDialogComponent {
  constructor( public dialogRef: MatDialogRef<ImageModalDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
