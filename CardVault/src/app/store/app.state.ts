import { InjectionToken } from '@angular/core';
import { CardListState, CardListReducer } from '@cv/CardList/store/cardlist.reducer';
import { DeckBuilderState, DeckBuilderReducer } from '@cv/DeckBuilder/store/deckBuilder.reducer';
import { ActionReducerMap } from '@ngrx/store';

export interface AppState {
  activePage: string;
  cardItems: CardListState;
  decks: DeckBuilderState;
}

export const rootReducer = {
  cardItems: CardListReducer,
  decks: DeckBuilderReducer
};

export const reducerToken = new InjectionToken<ActionReducerMap<AppState>>('Reducers');

export function getReducers() { return rootReducer }

export const reducerProvider = [
  { provide: reducerToken, useFactory: getReducers }
];