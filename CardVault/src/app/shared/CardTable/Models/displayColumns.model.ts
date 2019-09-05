export interface DisplayColumns {
  columnDef: string,
  headerText: string,
  editable: boolean
}

export const CardListColumns: DisplayColumns[] = [
  { columnDef: 'name', headerText: 'Name', editable: false },
  { columnDef: 'colorIdentity', headerText: 'Colors', editable: false },
  { columnDef: 'manaCost', headerText: 'Cost', editable: false },
  { columnDef: 'quantity', headerText: 'Own', editable: false },
  { columnDef: 'type', headerText: 'Type', editable: false },
  { columnDef: 'setName', headerText: 'Set', editable: false },
  { columnDef: 'rarity', headerText: 'Rarity', editable: false },
  { columnDef: 'text', headerText: 'Text', editable: false },
];

export const DeckListColumns: DisplayColumns[] = [
  { columnDef: 'name', headerText: 'Name', editable: false },
  { columnDef: 'colorIdentity', headerText: 'Colors', editable: false },
  { columnDef: 'manaCost', headerText: 'Cost', editable: false },
  { columnDef: 'quantity', headerText: 'Own', editable: true },
  { columnDef: 'type', headerText: 'Type', editable: false },
  { columnDef: 'text', headerText: 'Text', editable: false },
];