import { NgModule } from '@angular/core';
import { MaterialModule } from './material.module';
import { CardTableComponent } from './CardTable';
import { BrowserModule } from '@angular/platform-browser';

const COMPONENTS = [
  CardTableComponent
]

const MODULES = [
  BrowserModule,
  MaterialModule
];

@NgModule({
  declarations: [COMPONENTS],
  imports: [MODULES],
  exports: [MODULES, COMPONENTS],
  providers: [],
  bootstrap: [COMPONENTS]
})
export class SharedModule { }