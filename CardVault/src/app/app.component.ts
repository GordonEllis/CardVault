import { Component } from '@angular/core';
import * as CoreStore from '@cv/core/store';
import { Store } from '@ngrx/store';
import { AppState } from '@cv/state';

@Component({
  selector: 'card-vault',
  templateUrl: `app.component.html`,
})

export class AppComponent  { 
  activePage: string;

  constructor(private store: Store<AppState>) {
    store.select(CoreStore.getActivePage).subscribe(p => this.activePage = p);
  }

  setActivePage(page: string) {
    this.store.dispatch(new CoreStore.SetActivePage(page));
  }
}
