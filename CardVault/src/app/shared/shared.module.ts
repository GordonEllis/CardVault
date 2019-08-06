import { NgModule } from '@angular/core';
import { MaterialModule } from './material.module';
import { CardTableComponent } from './CardTable';

const COMPONENTS = [
  CardTableComponent
]

const MODULES = [
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