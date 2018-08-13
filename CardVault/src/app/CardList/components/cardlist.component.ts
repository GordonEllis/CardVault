import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog, MatSort } from '@angular/material';
import { DialogComponent } from './dialog';
import { Store } from '@ngrx/store';
import { AppState } from '@cv/core';
import { CardItem } from '@cv/CardList/Models';
import { GetCards } from '@cv/CardList/cardlist.actions';


@Component({
  selector: 'cardlist',
  templateUrl: `cardlist.component.html`,
})

export class CardListComponent implements OnInit {
  dataSource:  MatTableDataSource<CardItem>;
  displayedColumns: string[] = ['CardName', 'ManaCost', 'CMC', 'CardSet', 'Rarity'];
  
  @ViewChild(MatSort) sort: MatSort;

  constructor(public dialog: MatDialog,
              private store: Store<AppState>) {
    this.store.select(s => s.cardItems).subscribe(items =>{
      console.log(items.data);
      this.dataSource = new MatTableDataSource<CardItem>(items.data);
      this.dataSource.filterPredicate = (data: CardItem, filter: string) => 
      {
        console.log(data.Name);
       return  data.Name.toLowerCase().indexOf(filter) != -1;
      }
    })
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  ngOnInit(){
    this.store.dispatch(new GetCards());
  }

  addItem() {
    this.dialog.open(DialogComponent);
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }
}


