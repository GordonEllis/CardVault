import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

import { AppComponent }  from '@cv/app.component';
import { AppRoutingModule } from '@cv/app.routes';
import { SharedModule } from '@cv/shared';
import { rootReducer } from '@cv/store';
import { CardListModule } from '@cv/CardList';
import { DeckBuilderModule } from '@cv/DeckBuilder';

@NgModule({
  imports: [ 
    StoreModule.forRoot(rootReducer),
    EffectsModule.forRoot([]),
    BrowserModule,
    CardListModule,
    DeckBuilderModule,
    HttpClientModule,
    SharedModule,
    AppRoutingModule,
    //StoreRouterConnectingModule.forRoot(),
    //StoreDevtoolsModule.instrument({ maxAge: 50 }),
    //CoreModule,
    //FormsModule,
    //ReactiveFormsModule,
  ],
  providers: [ ],
  declarations: [ AppComponent ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
