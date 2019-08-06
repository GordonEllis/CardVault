import { CardListState, CardListReducer } from '@cv/CardList/store/cardlist.reducer';
import { DeckBuilderState, DeckBuilderReducer } from '@cv/DeckBuilder';

export interface AppState {
  activePage: string;
  cardItems: CardListState;
  decks: DeckBuilderState;
}

export const rootReducer = {
  cardItems: CardListReducer,
  decks: DeckBuilderReducer
};