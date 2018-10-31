import { CoreState } from "./core.reducer";
import { createFeatureSelector } from "@ngrx/store";
import { createSelector } from "@ngrx/store";

export const coreFeatureName = 'core';

export const getCoreState = createFeatureSelector<CoreState>(coreFeatureName);

export const getShowSideMenu = createSelector(
  getCoreState,
  (state: CoreState) => state.showSideMenu
)

export const getActivePage = createSelector(
  getCoreState,
  (state: CoreState) => state.activePage
)
