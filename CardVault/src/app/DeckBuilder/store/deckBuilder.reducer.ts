
import * as DeckBuilderActions from './deckBuilder.actions';
import { Deck, DeckCard, newDeck } from '@cv/DeckBuilder/models';

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
    case DeckBuilderActions.AddCardsToActiveDeck.TYPE: {
      const currentActiveDeck = state.activeEditDeck;
      const filterCards =  action.items.filter(c => !currentActiveDeck.deckCards.find(a => a.cardId === c.id ))
      currentActiveDeck.deckCards =
      [...currentActiveDeck.deckCards, ...filterCards.map(i => ({ deckId: -1, cardId: i.id, quantity: i.quantity }))];

      return {
        ...state,
        activeEditDeck:  currentActiveDeck
      };
    }
    case DeckBuilderActions.RemoveCardsFromActiveDeck.TYPE: {
      const currentActiveDeck = state.activeEditDeck;
      currentActiveDeck.deckCards = currentActiveDeck.deckCards.filter(c => !action.items.find(a => a.id === c.cardId ));
      
      return {
        ...state,
        activeEditDeck:  currentActiveDeck
      };
    }
    case DeckBuilderActions.DeleteDeckSuccess.TYPE: {
      const decks = state.data.filter(d => d.deckId !== action.deckId);
      return {
        ...state,
        data: decks
      };
    }
    case DeckBuilderActions.LoadDecksSuccess.TYPE: {
      return {
        ...state,
        data: action.items
      };
    }
    case DeckBuilderActions.SaveDeckSuccess.TYPE: {
      const decks = state.data.filter(d => d.deckId !== action.item.deckId);
      decks.push(action.item);
      return {
        ...state,
        data: decks,
        activeEditDeck: action.item
      };
    }
    case DeckBuilderActions.SetActiveDeck.TYPE: {
      const deck = state.data.find(d => d.deckId === action.deckId);
      
      return {
        ...state,
        activeEditDeck: deck
      };
    }
    case DeckBuilderActions.UpdateDeck.TYPE: {
      return {
        ...state,
        activeEditDeck: action.item
      };
    }

    default: {
      return state;
    }
  }
}