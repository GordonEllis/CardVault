import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@cv/shared';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { DeckBuilderComponent, BuiltDecksComponent } from '@cv/DeckBuilder/components';
import { DeckBuilderEffects, DeckBuilderReducer } from '@cv/DeckBuilder/store';
import { DeckBuilderService } from '@cv/DeckBuilder/service';

const COMPONENTS = [
  DeckBuilderComponent,
  BuiltDecksComponent,
]

@NgModule({
  declarations: [COMPONENTS],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    EffectsModule.forFeature([DeckBuilderEffects]),
    StoreModule.forRoot(DeckBuilderReducer)
  ],
  exports: [DeckBuilderComponent],
  entryComponents: [],
  providers: [DeckBuilderService],
  bootstrap: [DeckBuilderComponent]
})
export class DeckBuilderModule { }