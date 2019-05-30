import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog, MatSort } from '@angular/material';
import { DialogComponent } from './dialog';
import { Store } from '@ngrx/store';
import { AppState } from '@cv/state';
import { CardItem } from '@cv/CardList/models';
import { GetCards } from '@cv/CardList/store/cardlist.actions';

@Component({
  selector: 'cardlist',
  templateUrl: `cardlist.component.html`,
  styleUrls: ['./cardlist.component.scss'],
})

export class CardListComponent implements OnInit {
  dataSource:  MatTableDataSource<CardItem>;
  displayedColumns: string[] = ['CardName', 'Colors', 'ManaCost', 'Own', 'Type', 'CardSet', 'Rarity'];
  filterOption: string= 'CardName';
  
  @ViewChild(MatSort) sort: MatSort;

  constructor(public dialog: MatDialog, private store: Store<AppState>) {
    this.store.select(s => s.cardItems).subscribe(items => this.setDataSourceData(items.data) )
  }

  ngAfterViewInit() { this.dataSource.sort = this.sort; }

  ngOnInit(){
    this.store.dispatch(new GetCards());
  }

  addItem() { this.dialog.open(DialogComponent); }

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
