import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CardItem } from '@cv/CardList/models';
import { environment } from 'environments/environment';
import { Observable, of } from 'rxjs';

@Injectable()
export class CardListService {
  public constructor(private http: HttpClient) { }

  getItems(cardIds?: string[]): Observable<CardItem[]> {
    return this.http.get<CardItem[]>(environment.apiBase + 'cards');
  }

  addItem(item: CardItem): Observable<CardItem> {
    return of(item);
  }
}