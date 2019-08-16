import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CardListComponent } from './CardList';
import { AppComponent } from '@cv/app.component';
import { CreateDeckComponent, DeckBuilderComponent, BuiltDecksComponent } from '@cv/DeckBuilder';

const routes: Routes = [
  { path: 'list', component: CardListComponent },
  { path: 'newDeck', component: CreateDeckComponent },
  { path: 'newDeck/:deckId', component: CreateDeckComponent},
  { path: 'constructedDecks', component: BuiltDecksComponent },
  { path: 'constructedDecks/:deckId', component: BuiltDecksComponent },
  { path: '', redirectTo: 'list', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }