import { Observable } from "rxjs";
import { CardItem } from "@cv/CardSearch";

export interface ICardSearchService {
  getItems(cardIds?: string[]): Observable<CardItem[]>;
}