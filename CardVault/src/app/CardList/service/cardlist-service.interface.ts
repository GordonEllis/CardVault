import { Observable } from "rxjs";
import { CardItem } from "@cv/CardList";

export interface ICardListService {
  getItems(cardIds?: string[]): Observable<CardItem[]>;
}