import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog, MatSort } from '@angular/material';
import { Store } from '@ngrx/store';
import { AppState } from '@cv/state';
import { CardItem } from '@cv/CardSearch/models';
import { GetCards } from '@cv/CardSearch/store/cardsearch.actions';

@Component({
  selector: 'cardsearch',
  templateUrl: `cardsearch.component.html`,
})

export class CardSearchComponent implements OnInit {
  dataSource:  MatTableDataSource<CardItem>;
  displayedColumns: string[] = ['CardName', 'Colors', 'ManaCost', 'Own', 'Type', 'CardSet', 'Rarity'];
  filterOption: string= '';
  
  @ViewChild(MatSort) sort: MatSort;

  constructor(public dialog: MatDialog, private store: Store<AppState>) {
    this.store.select(s => s.cardItems).subscribe(items => this.setDataSourceData(items.data))
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  ngOnInit(){
    this.store.dispatch(new GetCards());
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
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
          filterData = data.colors.toString();
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
          filterData = data.typeLine;
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
