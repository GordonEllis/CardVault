import { Component, OnInit, ViewChild } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '@cv/store';

@Component({
  selector: 'deckBuilder',
  templateUrl: `deckBuilder.component.html`,
  styleUrls: ['./deckBuilder.component.scss'],
})

export class DeckBuilderComponent {
  constructor(private store: Store<AppState>) { }
}
