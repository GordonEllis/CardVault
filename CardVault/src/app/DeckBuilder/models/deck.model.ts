import { DeckCard } from "./deckCard.model";

export interface Deck {
  deckId: number,
  name: string,
  description: string,
  deckCards: DeckCard[]
}

export const newDeck: Deck = {
  deckId: -1,
  name: 'New Deck',
  description: null,
  deckCards: []
}