import { createSelector } from '@ngrx/store';
import { AppState } from '@cv/state';
import { CardListState } from './cardlist.reducer';

export const getCardListState = (state: AppState) => state.cardItems;

export const getCards = createSelector(
  getCardListState,
  (state: CardListState) => state.data
);
