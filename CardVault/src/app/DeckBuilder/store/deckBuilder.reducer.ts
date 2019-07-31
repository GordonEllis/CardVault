
import * as DeckBuilderActions from './deckBuilder.actions';
import { Deck } from '@cv/DeckBuilder/models';

export interface DeckBuilderState {
  data: Deck[];
  isLoading: boolean;
}

const initialState: DeckBuilderState = {
  data: [],
  isLoading: false
};

export function DeckBuilderReducer(state = initialState, action: DeckBuilderActions.DeckBulderActions): DeckBuilderState {
  switch (action.type) {
    default: {
      return state;
    }
  }
}