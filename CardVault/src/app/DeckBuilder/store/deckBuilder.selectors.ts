import { createSelector } from '@ngrx/store';
import { AppState } from '@cv/state';
import { DeckBuilderState } from './deckBuilder.reducer';
import { Deck } from '../models';
import { DeckCard } from '../models/deckCard.model';
import { getCards } from '@cv/CardList/store';
import { CardItem } from '@cv/CardList/models';

export const getDecksState = (state: AppState) => state.decks;

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
    deckCards.activeEditDeck.deckCards.forEach(d => {
      hydratedData.push(fullCardData.find(f => f.id === d.CardId));
    });

    return hydratedData;
  }
);
