import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { select, Store } from '@ngrx/store';
import { AppState } from '@cv/store';
import { CardTableComponent, CardListColumns, DisplayColumns } from '@cv/shared/CardTable';
import { FilterDialogComponent, FilterDialogConfig } from '@cv/shared/filterDialog';
import { CardItem } from '@cv/CardList/models';
import { GetCards, getCards } from '@cv/CardList/store';
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
  tableColumns: DisplayColumns[];
  
  @ViewChild('cardTable') cardTable: CardTableComponent;
  
  constructor(private store: Store<AppState>, private router: Router, protected dialog: MatDialog) { }
  
  ngOnInit(){ 
    this.tableColumns = CardListColumns;
    this.store.pipe(select(getCards)).subscribe(cards => this.setDataSourceData(cards));
    this.store.pipe(select(getActiveDeck)).subscribe(d => this.deckCreationActive = d.deckCards.length > 0);
  }

  addCardsToDeck() { 
    this.cardTable.addCardsToDeck();
    this.router.navigate(['/newDeck']);
  }

  toggleFilter() {
    const data: FilterDialogConfig = {
      title: `Add Filters`,
      items: this.tableColumns.map(c => c.headerText)
    };

    const dialogRef = this.dialog.open(FilterDialogComponent);
    dialogRef.afterClosed().subscribe((r: any) => {
      this.applyFilter(r);
    });
  }

  applyFilter(filterValue: any) {
    //let filter: CardItem
//    JSON.stringify(filter)
    
    filterValue.colors = '';
    filterValue.rarity = '';
    console.log(filterValue);
    console.log(this.dataSource);
    //this.dataSource.filter = filterValue.trim().toLowerCase(); 
    this.dataSource.filter = JSON.stringify(filterValue);
  }
  
  setFilterOption(option: string) { this.applyFilter(this.dataSource.filter) }

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
        case 'Text': {
          filterData = data.text;
          break;
        }
      }

      return filterData.toLowerCase().indexOf(filter) != -1};
  }
}
