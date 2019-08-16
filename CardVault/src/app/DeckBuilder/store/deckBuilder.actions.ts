import { Action } from '@ngrx/store';
import { CardItem } from '@cv/CardList/models';
import { Deck, DeckCard } from '@cv/DeckBuilder/models';


export class SetActiveDeck implements Action {
  public static readonly TYPE = '[DeckBuilder] Set Active Deck';
  readonly type = SetActiveDeck.TYPE;
  constructor(public deckId: number) { }
}

export class SaveDeck implements Action {
  public static readonly TYPE = '[DeckBuilder] Save Deck';
  readonly type = SaveDeck.TYPE;
}

export class SaveDeckSuccess implements Action {
  public static readonly TYPE = '[DeckBuilder] Save Deck Success';
  readonly type = SaveDeckSuccess.TYPE;
  constructor(public item: Deck) { }
}

export class CreateDeck implements Action {
  public static readonly TYPE = '[DeckBuilder] Create Deck';
  readonly type = CreateDeck.TYPE;
  constructor(public items: CardItem[]) { }
}

export class CreateDeckSuccess implements Action {
  public static readonly TYPE = '[DeckBuilder] Create Deck Success';
  readonly type = CreateDeckSuccess.TYPE;
  constructor(public items: DeckCard[]) { }
}

export class LoadDecks implements Action {
  public static readonly TYPE = '[DeckBuilder] Load Decks';
  readonly type = LoadDecks.TYPE;
}

export class LoadDecksSuccess implements Action {
  public static readonly TYPE = '[DeckBuilder] Load Decks Success';
  readonly type = LoadDecksSuccess.TYPE;
  constructor(public items: Deck[]) { }
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
  constructor(public deckId: number, public success: Boolean) { }
}

export type DeckBulderActions =
    SetActiveDeck |
    CreateDeck |
    CreateDeckSuccess |
    SaveDeck |
    SaveDeckSuccess |
    UpdateDeck |
    UpdateDeckSuccess |
    DeleteDeck |
    DeleteDeckSuccess |
    LoadDecks |
    LoadDecksSuccess;