import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MatTableDataSource, MatDialog, MatSort } from '@angular/material';
import { select, Store } from '@ngrx/store';
import { AppState } from '@cv/store';
import { CardItem } from '@cv/CardList/models';
import { Deck } from '@cv/DeckBuilder/models';
import { getActiveDeck, getActiveDeckCardFull } from '@cv/DeckBuilder/store/deckBuilder.selectors';
import { SaveDeck, SetActiveDeck, UpdateDeck } from '@cv/DeckBuilder/store/deckBuilder.actions';
import { ColumnUpdated, DeckListColumns, DisplayColumns } from '@cv/shared/CardTable';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'createDeck',
  templateUrl: `createDeck.component.html`,
  styleUrls: ['./createDeck.component.scss'],
})

export class CreateDeckComponent {
  deck: Deck;
  dataSource:  MatTableDataSource<CardItem>;
  filterOption: string= 'DeckName';
  tableColumns: DisplayColumns[];
  
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('deckName') deckName: ElementRef<HTMLInputElement>;
  @ViewChild('deckDescription') deckDescription: ElementRef<HTMLInputElement>;

  constructor(private store: Store<AppState>, private router: Router, private route: ActivatedRoute) {
    this.store.pipe(select(getActiveDeck)).subscribe(deck => this.deck = deck);
    this.store.pipe(select(getActiveDeckCardFull)).subscribe(info => this.setDataSourceData(info));
     this.route.paramMap.subscribe(params => {
       if(params.get('deckId')) { this.store.dispatch(new SetActiveDeck(+params.get('deckId'))); }
    });
  }

  ngOnInit(){ this.tableColumns = DeckListColumns }
  addCards() { this.router.navigate(['/list']) }
  saveDeck() { this.store.dispatch(new SaveDeck()) }
  setDataSourceData(deck: CardItem[]) { this.dataSource = new MatTableDataSource<CardItem>(deck) }
  updateActiveDeck() { this.store.dispatch(new UpdateDeck(this.deck)) }

  updateColumn(data: ColumnUpdated) {  
    this.deck.deckCards.find(d => d.cardId === data.value.id).quantity = data.value.quantity;
    this.updateActiveDeck();
  }
    
  nameChanged() { 
    this.deck.name = this.deckName.nativeElement.value;
    this.updateActiveDeck(); 
  }

  descriptionChanged() { 
    this.deck.description = this.deckDescription.nativeElement.value; 
    this.updateActiveDeck();
  }  
}
