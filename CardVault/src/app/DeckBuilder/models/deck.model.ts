import { CardItem } from "@cv/CardList";

export interface Deck {
  id: number,
  name: string,
  description: string,
  cards: CardItem[]
}