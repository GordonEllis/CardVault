import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnChanges, Output, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog, MatSort, MatPaginator } from '@angular/material';
import { select, Store } from '@ngrx/store';
import { AppState } from '@cv/store';
import { CardItem } from '@cv/CardList/models';
import { ColumnUpdated, DisplayColumns } from './Models'
import { GetCards, getCards } from '@cv/CardList/store/';
import { AddCardsToActiveDeck, RemoveCardsFromActiveDeck } from '@cv/DeckBuilder/store';

@Component({
  selector: 'cardTable',
  templateUrl: `cardTable.component.html`,
  styleUrls: ['./cardTable.component.scss'],
})

export class CardTableComponent implements OnChanges {
  @Input() dataSource: MatTableDataSource<CardItem>;
  @Input() tableColumns: DisplayColumns[];
  @Output() columnChange: EventEmitter<ColumnUpdated> = new EventEmitter<ColumnUpdated>();
  
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  
  selectedCards: SelectionModel<CardItem>;
  displayedColumns: string[] = ['Select', 'CardImage'];
  tableDisplayedColumns: DisplayColumns[];
  
  constructor(private store: Store<AppState>) { }
  
  ngOnInit() {
    this.selectedCards = new SelectionModel<CardItem>(true, []);
    this.tableDisplayedColumns = this.tableColumns; 
    const columns = this.tableColumns.map(c => c.columnDef);
    this.displayedColumns = [...this.displayedColumns, ...columns];
  }

  ngOnChanges(){ 
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;   
   }

  isEditable(column: string) { return this.tableColumns.find(c => c.columnDef === column).editable }
  columnEdited(column: string, value: any, card: CardItem) { 
    if(column === 'quantity') { 
      card[column] = value;
      const data: ColumnUpdated = {columnEdited: column, value: card}
      this.columnChange.emit(data);
    }
  }

  isAllSelected() {
    const numSelected = this.selectedCards.selected.length;
    const numRows = this.dataSource.filteredData.length;
    return numSelected === numRows;
  }

  selectAll() { 
      this.isAllSelected() ?
          this.selectedCards.clear() :
          this.dataSource.filteredData.forEach(row => this.selectedCards.select(row));
  }

  addCardsToDeck() { this.store.dispatch(new AddCardsToActiveDeck(this.selectedCards.selected)) }
  removeCardsFromDeck() { this.store.dispatch(new RemoveCardsFromActiveDeck(this.selectedCards.selected)) }
}