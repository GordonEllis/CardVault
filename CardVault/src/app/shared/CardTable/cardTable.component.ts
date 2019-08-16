import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnChanges, Output, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog, MatSort, MatPaginator } from '@angular/material';
import { select, Store } from '@ngrx/store';
import { AppState } from '@cv/store';
import { CardItem } from '@cv/CardList/models';
import { ColumnUpdated, DisplayColumns } from './Models'
import { GetCards, getCards } from '@cv/CardList/store/';
import { CreateDeck, UpdateDeck } from '@cv/DeckBuilder/store';

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
  tableDisplayedColumns: string[];
  
  constructor(private store: Store<AppState>) { }
  
  ngOnInit() {
    this.selectedCards = new SelectionModel<CardItem>(true, []);
    this.tableDisplayedColumns = this.tableColumns.map(c => c.columnDef);
    this.displayedColumns = [...this.displayedColumns, ...this.tableDisplayedColumns];
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

  addDeck() { this.store.dispatch(new CreateDeck(this.selectedCards.selected)) }
}