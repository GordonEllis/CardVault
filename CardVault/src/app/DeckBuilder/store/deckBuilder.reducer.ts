
import * as DeckBuilderActions from './deckBuilder.actions';
import { Deck, newDeck } from '@cv/DeckBuilder/models';
import { fulltoDeck } from '../util/cardInfoConvert';

export interface DeckBuilderState {
  data: Deck[];
  activeEditDeck: Deck;
  isLoading: boolean;
}

const initialState: DeckBuilderState = {
  data: [],
  activeEditDeck: newDeck,
  isLoading: false
};

export function DeckBuilderReducer(state = initialState, action: DeckBuilderActions.DeckBulderActions): DeckBuilderState {
  switch (action.type) {
    case DeckBuilderActions.CreateDeck.TYPE: {
      let currentActiveDeck = state.activeEditDeck;
      currentActiveDeck.deckCards = currentActiveDeck.deckCards.length > 1 ? 
                                fulltoDeck(state.activeEditDeck.id, action.items):
                                //currentActiveDeck.cards.push(action.items) :
                                fulltoDeck(state.activeEditDeck.id, action.items);
      return {
        ...state,
        activeEditDeck: currentActiveDeck
      };
    }
    default: {
      return state;
    }
  }
}