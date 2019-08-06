import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Deck } from '@cv/DeckBuilder';

@Injectable()
export class DeckBuilderService {
  public constructor(private http: HttpClient) { }

  saveDeck(deck: Deck): Observable<Boolean> {
    return this.http.post<Boolean>(environment.apiBase + 'decks', deck);
  }

  deleteDeck(deckId: number): Observable<Boolean> {
    return this.http.delete<Boolean>(environment.apiBase + 'decks', { params: { orderId: deckId.toString() }});
  }
}