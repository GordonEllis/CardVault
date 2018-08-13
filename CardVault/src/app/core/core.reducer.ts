
import { CardListState, CardListReducer } from '@cv/CardList/cardlist.reducer';

export interface AppState {
  cardItems: CardListState;
}

export const rootReducer = {
  cardItems: CardListReducer
};