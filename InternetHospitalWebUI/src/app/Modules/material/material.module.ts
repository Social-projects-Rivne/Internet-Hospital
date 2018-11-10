import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule, 
          MatIconModule, 
          MatDialogModule, 
          MatNativeDateModule,
          MatListModule,
          MatGridListModule,
          MatInputModule,
          MatFormFieldModule,
          MatRadioModule,
          MatSelectModule,
          MatDatepickerModule,
          MatCheckboxModule,
          MatButtonModule,
          MatSnackBarModule,
          MatCardModule,
          MatPaginatorModule,
          MatDividerModule,
          MatTableModule,
          MatMenuModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

const MAT_MODULS = [
    CommonModule,
    MatToolbarModule,
    MatGridListModule,
    MatFormFieldModule,
    MatInputModule,
    MatRadioModule,
    MatSelectModule,
    MatDatepickerModule,
    MatCheckboxModule,
    MatNativeDateModule,
    MatButtonModule,
    MatSnackBarModule,
    MatIconModule,
    MatDialogModule,
    MatCardModule,
    FlexLayoutModule,
    MatDividerModule,
    MatPaginatorModule,
    MatMenuModule,
    MatListModule,
    MatTableModule
  ]

@NgModule({
  imports: [MAT_MODULS],
  declarations: [],
  exports: [MAT_MODULS]
})
export class MaterialModule { }
