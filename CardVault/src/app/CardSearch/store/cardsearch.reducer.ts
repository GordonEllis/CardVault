
import * as CardSearchActions from './cardsearch.actions';
import { CardItem } from '@cv/CardSearch/models';

export interface CardSearchState {
  data: CardItem[];
  hasError: boolean;
  isLoading: boolean;
}

const initialState: CardSearchState = {
  data: [],
  hasError: false,
  isLoading: false
};

export function CardSearchReducer(state = initialState, action: CardSearchActions.CardActions): CardSearchState {
  switch (action.type) {
    

    case CardSearchActions.GetCardsSuccess.TYPE: {
      console.log(action.items);
        return {
          ...state,
          data: action.items
        };
      }
    case CardSearchActions.AddCardSuccess.TYPE: {
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