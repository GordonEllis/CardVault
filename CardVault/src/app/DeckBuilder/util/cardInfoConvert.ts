import { CardItem } from "@cv/CardList/models";
import { DeckCard } from "@cv/DeckBuilder/models";

export const fulltoDeck = (deckId: number, cardData: CardItem[], quantity?: number): DeckCard[] => {
 const deckCards: DeckCard[] = [];

 cardData.forEach(c => {
  const deckCard: DeckCard = { DeckId: deckId, CardId: c.id, Quantity: quantity ? quantity : 0 };
  deckCards.push(deckCard);
 });

 return deckCards;
}