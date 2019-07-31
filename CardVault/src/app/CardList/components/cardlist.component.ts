import {SelectionModel} from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog, MatSort, MatPaginator } from '@angular/material';
import { DialogComponent } from './dialog';
import { select, Store } from '@ngrx/store';
import { AppState } from '@cv/state';
import { CardItem } from '@cv/CardList/models';
import { GetCards, getCards } from '@cv/CardList/store/';

@Component({
  selector: 'cardlist',
  templateUrl: `cardlist.component.html`,
  styleUrls: ['./cardlist.component.scss'],
})

export class CardListComponent implements OnInit {
  dataSource:  MatTableDataSource<CardItem>;
  displayedColumns: string[] = ['Select', 'CardName', 'Colors', 'ManaCost', 'Own', 'Type', 'CardSet', 'Rarity'];
  filterOption: string= 'CardName';
  selectedCards: SelectionModel<CardItem>
  
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  
  constructor(public dialog: MatDialog, private store: Store<AppState>) {
    this.store.pipe(select(getCards)).subscribe(cards => this.setDataSourceData(cards));
  }
  
  ngOnInit(){
    this.store.dispatch(new GetCards());
  }

  ngAfterViewInit() { 
    this.dataSource.sort = this.sort; 
    this.dataSource.paginator = this.paginator;
  }

  addItem() { this.dialog.open(DialogComponent); }

  applyFilter(filterValue: string) { this.dataSource.filter = filterValue.trim().toLowerCase(); }

  setFilterOption(option: string) { 
    this.filterOption = option; 
    this.applyFilter(this.dataSource.filter);
    console.log(this.selectedCards.selected);
  }

  setDataSourceData(items: CardItem[]) {
    this.selectedCards = new SelectionModel<CardItem>(true, []);
    this.dataSource = new MatTableDataSource<CardItem>(items);
    this.dataSource.paginator = this.paginator;
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
