
import * as CardListActions from './cardlist.actions';
import { CardItem } from '@cv/CardList/models';

export interface CardListState {
  data: CardItem[];
  isLoading: boolean;
}

const initialState: CardListState = {
  data: [],
  isLoading: false
};

export function CardListReducer(state = initialState, action: CardListActions.CardActions): CardListState {
  switch (action.type) {
    case CardListActions.GetCardsSuccess.TYPE: {
      return {
        ...state,
        data: action.items
      };
    }
    default: {
      return state;
    }
  }
}