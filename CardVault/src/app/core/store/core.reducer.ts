import * as Actions from './core.actions';

export interface CoreState {
  showSideMenu: boolean;
}

export const initialState: CoreState = {
  showSideMenu: false,
};

export function coreReducer(state = initialState, action: Actions.CoreActions): CoreState {
  switch (action.type) {
    case Actions.CloseSideMenu.TYPE: {
      return {
        ...state,
        showSideMenu: false
      };
    }
    case Actions.OpenSideMenu.TYPE: {
      return {
        ...state,
        showSideMenu: true
      };
    }
    default: {
      return state;
    }
  }
}
