import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  CardListComponent,
  DialogComponent,
} from './components';
import { CardListEffects } from './cardlist.effects';
import { CardListReducer } from './cardlist.reducer';
import { SharedModule } from '@cv/shared';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

@NgModule({
  declarations: [
    CardListComponent,
    DialogComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    EffectsModule.forFeature([CardListEffects]),
    StoreModule.forRoot(CardListReducer)
  ],
  exports: [
    CardListComponent,
    DialogComponent,
  ],
  entryComponents: [DialogComponent],
  providers: [],
  bootstrap: [CardListComponent]
})
export class CardListModule { }