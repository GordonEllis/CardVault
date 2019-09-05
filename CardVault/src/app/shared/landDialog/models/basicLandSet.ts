import { LandSet, LandSetDefaults } from './landSet';

export interface BasicLandSet {
  forest: LandSet;
  swamp: LandSet;
  mountain: LandSet;
  island: LandSet;
  plain: LandSet;
  waste: LandSet;
}

export const BasicLandSetDefaults = {
  forest: LandSetDefaults[0],
  swamp: LandSetDefaults[1],
  mountain: LandSetDefaults[2],
  island: LandSetDefaults[3],
  plain: LandSetDefaults[4],
  waste: LandSetDefaults[5]
}

