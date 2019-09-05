import { Component } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { LandSet, LandSetDefaults } from './models';

@Component({
  selector: 'landDialogComponent',
  templateUrl: 'landDialog.component.html',
  styleUrls: ['./landDialog.component.scss'],
})

export class LandDialogComponent {
  basicLands: LandSet[] = LandSetDefaults;

  constructor(private dialogRef: MatDialogRef<LandDialogComponent>) { }

  onSubmit() {
    this.dialogRef.close(this.basicLands);
  }

  onCancel(): void {
    this.dialogRef.close(null);
  }
}