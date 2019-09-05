import { createSelector } from '@ngrx/store';
import { AppState } from '@cv/store';
import { CardListState } from './cardlist.reducer';

export const getCardListState = (state: AppState) => state.cardItems;

export const getLoadingState = createSelector(
  getCardListState,
  (state: CardListState) => state.isLoading
);

export const getCards = createSelector(
  getCardListState,
  (state: CardListState) => state.data
);
