import * as Actions from './core.actions';

export interface CoreState {
  activePage: string
  showSideMenu: boolean;
}

export const initialState: CoreState = {
  activePage: 'list',
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
    case Actions.SetActivePage.TYPE: {
      return {
        ...state,
        activePage: action.data
      };
    }
    default: {
      return state;
    }
  }
}
