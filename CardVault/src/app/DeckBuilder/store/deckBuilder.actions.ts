import { Action } from '@ngrx/store';
import { Deck } from '@cv/DeckBuilder/models';

export class AddDeck implements Action {
  public static readonly TYPE = '[DeckBuilder] Add Deck';
  readonly type = AddDeck.TYPE;
  constructor(public item: Deck) { }
}

export class AddDeckSuccess implements Action {
  public static readonly TYPE = '[DeckBuilder] Add Deck Success';
  readonly type = AddDeckSuccess.TYPE;
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
    AddDeck |
    AddDeckSuccess |
    UpdateDeck |
    UpdateDeckSuccess |
    DeleteDeck |
    DeleteDeckSuccess;