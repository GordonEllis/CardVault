import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { switchMap } from 'rxjs/operators';

import * as CardListActions from '@cv/CardList/store/cardlist.actions';
import { CardListService } from '@cv/CardList/service';
import { CardItem } from '@cv/CardList/models';

@Injectable()
export class CardListEffects {

  constructor(
    private action$: Actions,
    private service: CardListService,
  ) { }

  @Effect() getItems$ = this.action$.pipe(
    ofType(CardListActions.GetCards.TYPE),
    switchMap((action: CardListActions.GetCards) => 
      this.service.getItems(action.itemIds).pipe(switchMap((items: CardItem[]) => [new CardListActions.GetCardsSuccess(items)]))
    )
  );
}