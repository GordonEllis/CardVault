import { NgModule, Optional, SkipSelf } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { coreFeatureName, coreReducer } from './store';

const COMPONENTS = [];

@NgModule({
  declarations: [
    ...COMPONENTS
  ],
  imports: [
    EffectsModule.forFeature([]),
    StoreModule.forFeature(coreFeatureName, coreReducer),
  ],
  providers: [],
  exports: [
    ...COMPONENTS
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error(`CoreModule has already been loaded. Import Core modules in the AppModule only.`);
    }
  }
}
