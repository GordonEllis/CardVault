import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';
import { CardItem } from '@cv/CardList/models';
import { CardListMock } from './cardlist.mock';
import { ICardListService } from './cardlist-service.interface';

@Injectable()
export class CardListMockService implements ICardListService {
  getItems(cardIds?: string[]): Observable<CardItem[]> {
    return of(CardListMock);
  }
}