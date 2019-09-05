import { Action } from '@ngrx/store';

export class OpenSideMenu implements Action {
  public static readonly TYPE = '[Core] Open Side Menu';
  readonly type = OpenSideMenu.TYPE;
}

export class CloseSideMenu implements Action {
  public static readonly TYPE = '[Core] Close Side Menu';
  readonly type = CloseSideMenu.TYPE;
}

export type CoreActions =
  CloseSideMenu |
  OpenSideMenu;