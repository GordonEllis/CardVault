import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CardSearchComponent } from '@cv/CardSearch/components';
import { CardSearchEffects, CardSearchReducer } from '@cv/CardSearch/store';
import { SharedModule } from '@cv/shared';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

@NgModule({
  declarations: [
    CardSearchComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    EffectsModule.forFeature([CardSearchEffects]),
    StoreModule.forRoot(CardSearchReducer)
  ],
  exports: [
    CardSearchComponent
  ],
  entryComponents: [],
  providers: [],
  bootstrap: [CardSearchComponent]
})
export class CardSearchModule { }