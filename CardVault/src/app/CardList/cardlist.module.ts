import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CardListComponent, DialogComponent } from '@cv/CardList/components';
import { CardListEffects, CardListReducer } from '@cv/CardList/store';
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