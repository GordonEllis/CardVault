import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { AppComponent }  from './app.component';
import { SharedModule } from './shared';
import { EffectsModule } from '@ngrx/effects';
import { routerReducer, StoreRouterConnectingModule } from '@ngrx/router-store';
import { CardListModule } from '@cv/CardList';
import { HttpClientModule } from '@angular/common/http';
import { rootReducer } from './state';
import { CoreModule } from '@cv/core';
import { AppRoutingModule } from '@cv/app.routes';
import { DeckBuilderModule } from '@cv/DeckBuilder';

@NgModule({
  imports: [ 
    StoreModule.forRoot(rootReducer),
    StoreRouterConnectingModule.forRoot(),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({ maxAge: 50 }),
    BrowserModule,
    CardListModule,
    DeckBuilderModule,
    CoreModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    SharedModule,
    AppRoutingModule,
  ],
  providers: [ ],
  declarations: [ AppComponent ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
