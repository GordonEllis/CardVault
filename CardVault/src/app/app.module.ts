import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { StoreModule } from '@ngrx/store';

import { AppComponent }  from './app.component';
import { routes } from './app.routes';
import { SharedModule } from './shared';
import { EffectsModule } from '@ngrx/effects';
import { CardListEffects, CardListService, CardListModule, CardListMockService } from '@cv/CardList';
import { HttpClientModule } from '@angular/common/http';
import { rootReducer } from './state';
import { CoreModule } from '@cv/core';

@NgModule({
  imports: [ 
    BrowserModule,
    CardListModule,
    CoreModule,
    EffectsModule.forRoot([CardListEffects]),
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    SharedModule,
    StoreModule.forRoot(rootReducer),
  ],
  providers: [ 
    CardListService,
    CardListMockService
  ],
  declarations: [ AppComponent ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
