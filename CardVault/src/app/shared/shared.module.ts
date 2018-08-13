import { NgModule } from '@angular/core';
import { MaterialModule } from './material.module';

const modules = [
  MaterialModule
];

@NgModule({
  declarations: [],
  imports: modules,
  exports: modules,
  providers: [],
  bootstrap: []
})
export class SharedModule { }