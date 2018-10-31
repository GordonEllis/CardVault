import { CardListState, CardListReducer } from '@cv/CardList/store/cardlist.reducer';
import { CardSearchState, CardSearchReducer } from '@cv/CardSearch/store/cardsearch.reducer';

export interface AppState {
  activePage: string;
  cardItems: CardListState;
  searchItems: CardSearchState;
}

export const rootReducer = {
  cardItems: CardListReducer,
  searchItems: CardSearchReducer
};