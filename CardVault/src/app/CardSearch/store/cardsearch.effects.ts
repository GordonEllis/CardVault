import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { map, catchError, mergeMap } from 'rxjs/operators';
import { Observable } from 'rxjs';

import * as CardSearchActions from './cardsearch.actions';
import { CardSearchService, CardSearchMockService} from '@cv/CardSearch/service';

@Injectable()
export class CardSearchEffects {

  constructor(
    private action$: Actions,
    //private service: CardSearchService,
    private service: CardSearchMockService
  ) { }

  @Effect() getItems$ = this.action$.pipe(
    ofType(CardSearchActions.GetCards.TYPE),
    mergeMap((action: CardSearchActions.GetCards) =>
      this.service.getItems(action.itemIds).pipe(
        map(items => new CardSearchActions.GetCardsSuccess(items))
      )
    )
  );
}