import { Component, OnInit } from '@angular/core';
import * as CoreStore from '@cv/core/store';
import { Store } from '@ngrx/store';
import { AppState } from '@cv/store';
import { Router } from '@angular/router';
import { GetCards } from '@cv/CardList/store/';
import { LoadDecks } from '@cv/DeckBuilder/store/';

@Component({
  selector: 'card-vault',
  templateUrl: `app.component.html`,
  styleUrls: ['./app.component.scss'],
})

export class AppComponent  implements OnInit { 
  constructor(private router: Router, private store: Store<AppState>) { }

  setActivePage(page: string) { }

  ngOnInit() {
    this.store.dispatch(new GetCards());
    this.store.dispatch(new LoadDecks());
  }
}
