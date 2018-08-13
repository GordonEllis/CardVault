import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';

import { AppState } from '@cv/core';
import { AddCard } from '@cv/CardList/cardlist.actions';
import { CardItem } from '@cv/CardList/models';

@Component({
  selector: 'dialogComponent',
  templateUrl: 'dialog.component.html',
})

export class DialogComponent {
  cardForm: FormGroup;

  constructor(private dialogRef: MatDialogRef<DialogComponent>,
              private formBuilder: FormBuilder,
              private store: Store<AppState> ){
    this.cardForm = this.formBuilder.group({
      CardName: ['', Validators.required],
      ManaCost: [0, Validators.required],
    })
  }

  onSubmit() {
    var newCard: CardItem = this.cardForm.value;
    this.store.dispatch(new AddCard(newCard));
    this.onNoClick();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}