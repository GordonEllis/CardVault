import { CardItem } from "@cv/CardList/models";
import { DeckCard } from "@cv/DeckBuilder/models";

export const fulltoDeck = (deckId: number, cardData: CardItem[], quantity?: number): DeckCard[] => {
 const deckCards: DeckCard[] = [];

 cardData.forEach(c => deckCards.push({ deckId: deckId, cardId: c.id, quantity: quantity ? quantity : 0 }));

 return deckCards;
}