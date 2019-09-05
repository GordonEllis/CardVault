import { Component, Input, Inject, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, FormControl } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FilterDialogConfig, FilterSetting } from './models';
import { LandSet, LandSetDefaults } from '@cv/shared/landDialog/models';

@Component({
  selector: 'filterDialogComponent',
  templateUrl: 'filterDialog.component.html',
  styleUrls: ['./filterDialog.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class FilterDialogComponent {
  colors: LandSet[] = LandSetDefaults;
  items: string[];
  filterSetting: FormGroup;

  constructor(private dialogRef: MatDialogRef<FilterDialogComponent>, form: FormBuilder) {
    this.filterSetting = form.group({
      name: '',
      colors: new FormControl([]),
      cost: '',
      type: '',
      rarity: [],
      text: ''
    });
  }

  onSubmit() {
    this.dialogRef.close(this.filterSetting.value);
  }

  onNoClick(): void {
    this.dialogRef.close(null);
  }
}