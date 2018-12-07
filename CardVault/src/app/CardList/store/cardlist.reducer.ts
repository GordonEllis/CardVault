
import * as CardListActions from './cardlist.actions';
import { CardItem } from '@cv/CardList/models';

export interface CardListState {
  data: CardItem[];
  hasError: boolean;
  isLoading: boolean;
}

const initialState: CardListState = {
  data: [],
  hasError: false,
  isLoading: false
};

export function CardListReducer(state = initialState, action: CardListActions.CardActions): CardListState {
  switch (action.type) {
    

    case CardListActions.GetCardsSuccess.TYPE: {
      console.log(action.items[0])
        return {
          ...state,
          data: action.items
        };
      }
    case CardListActions.AddCardSuccess.TYPE: {
      return {
        ...state,
        hasError: false,
        data: state.data.concat([action.item])
      };
    }
    default: {
      return state;
    }
  }
}