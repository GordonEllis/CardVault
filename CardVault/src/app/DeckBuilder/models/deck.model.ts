import { DeckCard } from "./deckCard.model";

export interface Deck {
  id: number,
  name: string,
  description: string,
  deckCards: DeckCard[]
}

export const newDeck: Deck = {
  id: -1,
  name: 'New Deck',
  description: null,
  deckCards: []
}