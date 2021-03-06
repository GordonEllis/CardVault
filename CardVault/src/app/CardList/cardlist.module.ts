import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import * as Components from '@cv/CardList/components';
import { CardListService } from '@cv/CardList/service';
import { CardListEffects, CardListReducer } from '@cv/CardList/store';
import { SharedModule } from '@cv/shared';

import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

const COMPONENTS = [
  Components.CardListComponent,
  Components.DialogComponent,
];

@NgModule({
  declarations: COMPONENTS,
  exports: COMPONENTS,
  imports: [    
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    SharedModule,
    EffectsModule.forFeature([CardListEffects]),
  ],
  providers: [CardListService],
  entryComponents: [],
})

export class CardListModule { }