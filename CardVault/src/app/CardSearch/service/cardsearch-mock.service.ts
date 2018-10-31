import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';
import { CardItem } from '@cv/CardSearch/models';
import { CardSearchMock } from './cardsearch.mock';
import { ICardSearchService } from './cardsearch-service.interface';

@Injectable()
export class CardSearchMockService implements ICardSearchService {
  getItems(cardIds?: string[]): Observable<CardItem[]> {
    return of(CardSearchMock);
  }
}