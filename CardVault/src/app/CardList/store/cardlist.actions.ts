import { Action } from '@ngrx/store';
import { CardItem } from '@cv/CardList/models';

export class AddCard implements Action {
  public static readonly TYPE = '[CardList] Add Card';
  readonly type = AddCard.TYPE;
  constructor(public item: CardItem) { }
}

export class AddCardSuccess implements Action {
  public static readonly TYPE = '[CardList] Add Card Success';
  readonly type = AddCardSuccess.TYPE;
  constructor(public item: CardItem) { }
}

export class GetCards implements Action {
  public static readonly TYPE = '[CardList] Get Cards';
  readonly type = GetCards.TYPE;
  constructor(public itemIds?: string[]) { }
}

export class GetCardsSuccess implements Action {
  public static readonly TYPE = '[CardList] Get Cards Success';
  readonly type = GetCardsSuccess.TYPE;
  constructor(public items: CardItem[]) { }
}

export type CardActions =
    AddCard |
    AddCardSuccess|
    GetCards|
    GetCardsSuccess;