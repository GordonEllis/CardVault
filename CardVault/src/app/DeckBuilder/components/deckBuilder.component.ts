import { Component, OnInit, ViewChild } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from '@cv/state';

@Component({
  selector: 'deckBuilder',
  templateUrl: `deckBuilder.component.html`,
  styleUrls: ['./deckBuilder.component.scss'],
})

export class DeckBuilderComponent {
  constructor(private store: Store<AppState>) { }
}
