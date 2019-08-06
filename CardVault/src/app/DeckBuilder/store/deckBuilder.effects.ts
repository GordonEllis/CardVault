import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { map, catchError, mergeMap, switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';

import * as DeckBuilderActions from '@cv/DeckBuilder/store/deckBuilder.actions';
import { DeckBuilderService } from '@cv/DeckBuilder/service';

@Injectable()
export class DeckBuilderEffects {

  constructor(
    private action$: Actions,
    private service: DeckBuilderService,
  ) { }

  @Effect() addDeck$ = this.action$.pipe(
    ofType(DeckBuilderActions.SaveDeck.TYPE),
    switchMap((action: DeckBuilderActions.SaveDeck) => {
      console.log(action.item);
      return [];
      //return this.service.saveDeck(action.item).pipe(switchMap((items: Boolean) => [new DeckBuilderActions.AddDeckSuccess(items)]));
    })
  );
}