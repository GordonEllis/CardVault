import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { map, catchError, mergeMap } from 'rxjs/operators';
import { Observable } from 'rxjs';

import * as CardListActions from './cardlist.actions';
import { CardListService, CardListMockService} from '@cv/CardList/service';

@Injectable()
export class CardListEffects {

  constructor(
    private action$: Actions,
    //private service: CardListService,
    private service: CardListMockService
  ) { }

  @Effect() getItems$ = this.action$.pipe(
    ofType(CardListActions.GetCards.TYPE),
    mergeMap((action: CardListActions.GetCards) =>
      this.service.getItems(action.itemIds).pipe(
        map(items => new CardListActions.GetCardsSuccess(items))
      )
    )
  );

  // @Effect() addItem$ = this.action$.pipe(
  //   ofType(CardListActions.AddCard.TYPE),
  //   mergeMap((action: CardListActions.AddCard) =>
  //     this.service.addItem(action.item)
  //     .pipe(
  //       map(item => new CardListActions.AddCardSuccess(item))
  //     )
  //   )
  // );
}