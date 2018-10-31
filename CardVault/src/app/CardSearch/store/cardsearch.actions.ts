import { Action } from '@ngrx/store';
import { CardItem } from '@cv/CardSearch/models';

export class AddCard implements Action {
  public static readonly TYPE = '[CardSearch] Add Card';
  readonly type = AddCard.TYPE;
  constructor(public item: CardItem) { }
}

export class AddCardSuccess implements Action {
  public static readonly TYPE = '[CardSearch] Add Card Success';
  readonly type = AddCardSuccess.TYPE;
  constructor(public item: CardItem) { }
}

export class GetCards implements Action {
  public static readonly TYPE = '[CardSearch] Get Cards';
  readonly type = GetCards.TYPE;
  constructor(public itemIds?: string[]) { }
}

export class GetCardsSuccess implements Action {
  public static readonly TYPE = '[CardSearch] Get Cards Success';
  readonly type = GetCardsSuccess.TYPE;
  constructor(public items: CardItem[]) { }
}

export type CardActions =
    AddCard |
    AddCardSuccess|
    GetCards|
    GetCardsSuccess;