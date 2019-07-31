import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog, MatSort } from '@angular/material';
import { Store } from '@ngrx/store';
import { AppState } from '@cv/state';
import { Deck } from '@cv/DeckBuilder/models'

@Component({
  selector: 'builtDecks',
  templateUrl: `builtDecks.component.html`,
  styleUrls: ['./builtDecks.component.scss'],
})

export class BuiltDecksComponent {
  dataSource:  MatTableDataSource<Deck>;
  displayedColumns: string[] = ['DeckName', 'DeckDescription', 'ViewDeck', 'AddCards', 'RemoveDeck', 'Export'];
  filterOption: string= 'DeckName';
  
  @ViewChild(MatSort) sort: MatSort;

  constructor(public dialog: MatDialog, private store: Store<AppState>) {
    //this.store.select(s => s.g).subscribe(items => this.setDataSourceData(items.data) )
    this.dataSource = new MatTableDataSource<Deck>([{ id: 1, name: 'test', description: ' test', cards: []}]);
  }

  // ngAfterViewInit() { this.dataSource.sort = this.sort; }
  // addItem() { this.dialog.open(DialogComponent); }

  // applyFilter(filterValue: string) { this.dataSource.filter = 'DeckName';  }

  setDataSourceData(decks: Deck[]) {
    
    this.dataSource.filterPredicate = (data: Deck, filter: string) => {
    return data.name.toLowerCase().indexOf(filter) != -1};
  }
}
