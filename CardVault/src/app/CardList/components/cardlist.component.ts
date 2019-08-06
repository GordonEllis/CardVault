import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { select, Store } from '@ngrx/store';
import { AppState } from '@cv/state';
import { CardTableComponent } from '@cv/shared/CardTable';
import { CardItem } from '@cv/CardList/models';
import { GetCards, getCards } from '@cv/CardList/store/';
import { Router } from '@angular/router';
import { getActiveDeck } from '@cv/DeckBuilder';

@Component({
  selector: 'cardlist',
  templateUrl: `cardlist.component.html`,
  styleUrls: ['./cardlist.component.scss'],
})

export class CardListComponent implements OnInit {
  dataSource: MatTableDataSource<CardItem>;
  deckCreationActive: boolean = false;
  filterOption: string = 'CardName';
  
  @ViewChild('cardTable') cardTable: CardTableComponent;
  
  constructor(public dialog: MatDialog, private store: Store<AppState>, private router: Router) {
    this.store.pipe(select(getCards)).subscribe(cards => this.setDataSourceData(cards));
    this.store.pipe(select(getActiveDeck)).subscribe(d => this.deckCreationActive = d.deckCards.length > 0);
  }
  
  ngOnInit(){ this.store.dispatch(new GetCards()); }

  addDeck() { 
    this.cardTable.addDeck();
    this.router.navigate(['/newDeck']);
  }

  applyFilter(filterValue: string) { this.dataSource.filter = filterValue.trim().toLowerCase(); }

  setFilterOption(option: string) { 
    this.filterOption = option; 
    this.applyFilter(this.dataSource.filter);
  }

  setDataSourceData(items: CardItem[]) {
    this.dataSource = new MatTableDataSource<CardItem>(items);
    this.dataSource.filterPredicate = (data: CardItem, filter: string) => {
    let filterData;

      switch(this.filterOption) {
        case 'CardName': {
          filterData = data.name;
          break;
        }
        case 'Colors': {
          filterData = data.colorIdentity;
          break;
        }
        case 'ManaCost': {
          filterData = data.manaCost;
          break;
        }
        case 'Own': {
          filterData = data.quantity;
          break;
        }
        case 'Type': {
          filterData = data.type;
          break;
        }
        case 'CardSet': {
          filterData = data.setName;
          break;
        }
        case 'Rarity': {
          filterData = data.rarity;
          break;
        }
      }

      return filterData.toLowerCase().indexOf(filter) != -1};
  }
}
