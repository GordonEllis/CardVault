import { Component } from '@angular/core';
import * as CoreStore from '@cv/core/store';
import { Store } from '@ngrx/store';
import { AppState } from '@cv/state';
import { Router } from '@angular/router';

@Component({
  selector: 'card-vault',
  templateUrl: `app.component.html`,
})

export class AppComponent { 
  constructor(private router: Router) { }

  setActivePage(page: string) { }
}
