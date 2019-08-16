import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog, MatSort } from '@angular/material';
import { Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { AppState } from '@cv/store';
import { Deck } from '@cv/DeckBuilder/models'
import { DeleteDeck, getDecks } from '@cv/DeckBuilder/store'


@Component({
  selector: 'builtDecks',
  templateUrl: `builtDecks.component.html`,
  styleUrls: ['./builtDecks.component.scss'],
})

export class BuiltDecksComponent {
  dataSource:  MatTableDataSource<Deck>;
  displayedColumns: string[] = ['DeckName', 'DeckDescription', 'ViewDeck', 'AddCards', 'RemoveDeck', 'Export'];
  
  @ViewChild(MatSort) sort: MatSort;

  constructor(public dialog: MatDialog, private store: Store<AppState>, private router: Router,) {
    this.store.pipe(select(getDecks)).subscribe(decks => this.dataSource = new MatTableDataSource<Deck>(decks));
  }

  ngOnInit() { }

  viewDeck(deckId: number) {
    this.router.navigate(['/newDeck', { deckId: deckId }]);
  }

  deleteDeck(deckId: number) { this.store.dispatch(new DeleteDeck(deckId)) }

  addCards(deckId: number) {
    
  }
}
