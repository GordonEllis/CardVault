import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { Action, select, Store } from '@ngrx/store';
import { map, catchError, mergeMap, switchMap, withLatestFrom } from 'rxjs/operators';
import { Observable } from 'rxjs';

import * as DeckBuilderActions from '@cv/DeckBuilder/store/deckBuilder.actions';
import * as DeckBuilderSelectors from '@cv/DeckBuilder/store/deckBuilder.selectors';
import { DeckBuilderService } from '@cv/DeckBuilder/service';
import { Deck, DeckCard } from '@cv/DeckBuilder/models';
import { AppState } from '@cv/store';

@Injectable()
export class DeckBuilderEffects {
  constructor(
    private action$: Actions,
    private store$: Store<AppState>,
    private service: DeckBuilderService,
  ) { }


  @Effect() saveDeck$ = this.action$.pipe(
    ofType(DeckBuilderActions.SaveDeck.TYPE),
    withLatestFrom(this.store$.pipe(select(DeckBuilderSelectors.getActiveDeck))),
    switchMap(([action, deck]: [DeckBuilderActions.SaveDeck, Deck]) => {
      return this.service.saveDeck(deck).pipe(map((item: Deck) => new DeckBuilderActions.SaveDeckSuccess(item)));
    })
  );

  @Effect() deleteDeck$ = this.action$.pipe(
    ofType(DeckBuilderActions.DeleteDeck.TYPE),
    switchMap((action: DeckBuilderActions.DeleteDeck) => {
      return this.service.deleteDeck(action.deckId).pipe(
        map((success: boolean) => new DeckBuilderActions.DeleteDeckSuccess(action.deckId, success))
      );
    })
  );

  @Effect() loadDecks$ = this.action$.pipe(
    ofType(DeckBuilderActions.LoadDecks.TYPE),
    switchMap((action: DeckBuilderActions.LoadDecks) => {
      return this.service.loadDecks().pipe(map((items: Deck[]) => new DeckBuilderActions.LoadDecksSuccess(items)));
    })
  );
}