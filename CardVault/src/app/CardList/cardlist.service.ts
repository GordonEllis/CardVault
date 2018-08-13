import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CardItem } from './models';
import { environment } from '../../environments/environment';
import { Observable, of } from 'rxjs';

@Injectable()
export class CardListService {
  public constructor(private http: HttpClient) { }

  getItems(cardIds?: string[]): Observable<CardItem[]> {
    cardIds = ['c3dab325-8f4f-4288-9f3f-960e52b4335b'];
    console.log(cardIds); 
  return this.http.get<CardItem[]>(environment.apiBase + 'cards', { params: { cardIds } });
  }

  addItem(item: CardItem): Observable<CardItem> {
    return of(item);
  }
}