import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnChanges, Output, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog, MatSort, MatPaginator } from '@angular/material';
import { select, Store } from '@ngrx/store';
import { AppState } from '@cv/state';
import { CardItem } from '@cv/CardList/models';
import { GetCards, getCards } from '@cv/CardList/store/';
import { CreateDeck } from '@cv/DeckBuilder/store';

@Component({
  selector: 'cardTable',
  templateUrl: `cardTable.component.html`,
  styleUrls: ['./cardTable.component.scss'],
})

export class CardTableComponent implements OnChanges {
  @Input() dataSource:  MatTableDataSource<CardItem>;
  
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  
  selectedCards: SelectionModel<CardItem>;
  displayedColumns: string[] = ['Select', 'CardImage', 'CardName', 'Colors', 'ManaCost', 'Own', 'Type', 'CardSet', 'Rarity'];
  
  constructor(private store: Store<AppState>) { }
  
  ngOnInit() {
    this.selectedCards = new SelectionModel<CardItem>(true, []);
  }

  ngOnChanges(){ 
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;   
   }

  addDeck() { this.store.dispatch(new CreateDeck(this.selectedCards.selected)); }
}