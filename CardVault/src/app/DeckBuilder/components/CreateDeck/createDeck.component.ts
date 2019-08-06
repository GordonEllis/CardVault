import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MatTableDataSource, MatDialog, MatSort } from '@angular/material';
import { select, Store } from '@ngrx/store';
import { AppState } from '@cv/state';
import { CardItem } from '@cv/CardList/models';
import { Deck, DeckCard } from '@cv/DeckBuilder/models';
import { getActiveDeck, getActiveDeckCardFull } from '@cv/DeckBuilder/store/deckBuilder.selectors';
import { SaveDeck } from '@cv/DeckBuilder/store/deckBuilder.actions';
import { Router } from '@angular/router';

@Component({
  selector: 'createDeck',
  templateUrl: `createDeck.component.html`,
  styleUrls: ['./createDeck.component.scss'],
})

export class CreateDeckComponent {
  deck: Deck;
  dataSource:  MatTableDataSource<CardItem>;
  filterOption: string= 'DeckName';
  
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('deckName') deckName: ElementRef<HTMLInputElement>;
  @ViewChild('deckDescription') deckDescription: ElementRef<HTMLInputElement>;

  constructor(private store: Store<AppState>, private router: Router) {
    this.store.pipe(select(getActiveDeck)).subscribe(deck => this.deck = deck);
    this.store.pipe(select(getActiveDeckCardFull)).subscribe(info => this.setDataSourceData(info));
  }

  addCards() {
    this.router.navigate(['/list']);
  }

  saveDeck() { 
    this.store.dispatch(new SaveDeck(this.deck));
  }
    
  nameChanged() { this.deck.name = this.deckName.nativeElement.value; }
  descriptionChanged() { this.deck.description = this.deckDescription.nativeElement.value; }
  setDataSourceData(deck: CardItem[]) { this.dataSource = new MatTableDataSource<CardItem>(deck); }
}
