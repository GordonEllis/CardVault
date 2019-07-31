import { CardListState, CardListReducer } from '@cv/CardList/store/cardlist.reducer';

export interface AppState {
  activePage: string;
  cardItems: CardListState;
}

export const rootReducer = {
  cardItems: CardListReducer,
};