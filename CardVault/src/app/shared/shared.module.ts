import { NgModule } from '@angular/core';
import { MaterialModule } from './material.module';
import { CardTableComponent } from './CardTable';
import { FilterDialogComponent } from './filterDialog';
import { LandDialogComponent } from './landDialog';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';

const COMPONENTS = [
  CardTableComponent,
  FilterDialogComponent,
  LandDialogComponent,
]

const MODULES = [
  BrowserModule,
  FormsModule,
  MaterialModule,
  ReactiveFormsModule
];

@NgModule({
  declarations: [COMPONENTS],
  imports: [MODULES],
  exports: [MODULES, COMPONENTS],
  providers: [],
  bootstrap: [COMPONENTS]
})
export class SharedModule { }