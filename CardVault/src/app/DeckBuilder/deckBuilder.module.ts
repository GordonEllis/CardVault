import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@cv/shared';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { DeckBuilderComponent, BuiltDecksComponent, CreateDeckComponent } from '@cv/DeckBuilder/components';
import { DeckBuilderEffects, DeckBuilderReducer } from '@cv/DeckBuilder/store';
import { DeckBuilderService } from '@cv/DeckBuilder/service';

const COMPONENTS = [
  CreateDeckComponent,
  DeckBuilderComponent,
  BuiltDecksComponent,
]

@NgModule({
  declarations: COMPONENTS,
  exports: COMPONENTS,
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    StoreModule.forFeature('Deck', DeckBuilderReducer),
    EffectsModule.forFeature([DeckBuilderEffects]),
  ],
  providers: [DeckBuilderService],
  entryComponents: [],
})
export class DeckBuilderModule { }