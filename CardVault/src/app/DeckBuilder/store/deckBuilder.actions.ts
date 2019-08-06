import { Action } from '@ngrx/store';
import { CardItem } from '@cv/CardList/models';
import { Deck } from '@cv/DeckBuilder/models';

export class SaveDeck implements Action {
  public static readonly TYPE = '[DeckBuilder] Save Deck';
  readonly type = SaveDeck.TYPE;
  constructor(public item: Deck) { }
}

export class SaveDeckSuccess implements Action {
  public static readonly TYPE = '[DeckBuilder] Save Deck Success';
  readonly type = SaveDeckSuccess.TYPE;
  constructor(public success: Boolean) { }
}

export class CreateDeck implements Action {
  public static readonly TYPE = '[DeckBuilder] Create Deck';
  readonly type = CreateDeck.TYPE;
  constructor(public items: CardItem[]) { }
}

export class CreateDeckSuccess implements Action {
  public static readonly TYPE = '[DeckBuilder] Create Deck Success';
  readonly type = CreateDeckSuccess.TYPE;
  constructor(public success: Boolean) { }
}

export class UpdateDeck implements Action {
  public static readonly TYPE = '[DeckBuilder] Update Deck';
  readonly type = UpdateDeck.TYPE;
  constructor(public item: Deck) { }
}

export class UpdateDeckSuccess implements Action {
  public static readonly TYPE = '[DeckBuilder] Update Deck Success';
  readonly type = UpdateDeckSuccess.TYPE;
  constructor(public success: Boolean) { }
}

export class DeleteDeck implements Action {
  public static readonly TYPE = '[DeckBuilder] Delete Deck';
  readonly type = DeleteDeck.TYPE;
  constructor(public deckId: number) { }
}

export class DeleteDeckSuccess implements Action {
  public static readonly TYPE = '[DeckBuilder] Delete Deck Success';
  readonly type = DeleteDeckSuccess.TYPE;
  constructor(public success: Boolean) { }
}

export type DeckBulderActions =
    SaveDeck |
    SaveDeckSuccess |
    CreateDeck |
    UpdateDeck |
    UpdateDeckSuccess |
    DeleteDeck |
    DeleteDeckSuccess;