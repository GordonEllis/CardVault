import * as CardList from '@cv/CardList/store';
import * as DeckBuilder from '@cv/DeckBuilder/store';
import { createSelector } from '@ngrx/store';

export const getLoadingState = createSelector(
  CardList.getLoadingState,
  DeckBuilder.getLoadingState,
  (cards: boolean, decks: boolean) => cards || decks
);
