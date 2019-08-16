import { createSelector } from '@ngrx/store';
import { AppState } from '@cv/store';
import { DeckBuilderState } from './deckBuilder.reducer';
import { Deck, DeckCard } from '../models';
import { getCards } from '@cv/CardList/store';
import { CardItem } from '@cv/CardList/models';

export const getDecksState = (state: AppState) => state.decks;

export const getLoadingState = createSelector(
  getDecksState,
  (state: DeckBuilderState) => state.isLoading
);

export const getDecks = createSelector(
  getDecksState,
  (state: DeckBuilderState) => state.data
);

export const getActiveDeck = createSelector(
  getDecksState,
  (state: DeckBuilderState) => state.activeEditDeck
);

export const getActiveDeckCards = createSelector(
  getDecksState,
  (state: DeckBuilderState) => state.activeEditDeck.deckCards
);

export const getActiveDeckCardFull = createSelector(
  getDecksState,
  getCards,
  (deckCards: DeckBuilderState,
  fullCardData: CardItem[]) => {
    const hydratedData: CardItem[] = [];
    console.log(deckCards.activeEditDeck.deckCards);
    if(deckCards.activeEditDeck.deckCards) {
      deckCards.activeEditDeck.deckCards.forEach(d => {
        let fullDeckData: CardItem = fullCardData.find(f => f.id === d.cardId);
        fullDeckData.quantity = d.quantity;
        hydratedData.push(fullDeckData);
      });
    }
    
    return hydratedData;
  }
);
