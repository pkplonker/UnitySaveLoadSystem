# UnitySaveLoadSystem

## Files in the editor folder **must** be placed in an editor folder within unity.

## Files in scripts can be placed where ever you choose.

### File Summary
#### Interfaces

- ISaveLoad is an interface to be implemented on any components that have data to save
- ISaveLoadIO is an interface for handling external input/output

#### Clases
- SaveableGameObject should be included on any gameobject that has a component implementing ISaveLoad. Observer pattern
- SavingSystem is the core functionality with public functions to save/load. Singleton pattern
- SavefileHandlder handles file IO
- SaveLoadExternalMediator can be modified to change which class is currently being used as IO handler by changing to any class implementing ISaveLoadIO. Mediator Pattenen.
- SerializableVector is a simple wrapper for Vector3 to allow serialization of Vector data


Inspired by https://www.youtube.com/watch?v=yIsoAuOOG7Q

#### Improvements made
-  Automatic ID generation
-  Removal of FindObjectsOfType in favor of observer pattern
-  Mediator pattern for external save load
-  Editor functionality
-  Safety/Null checks
-  Event actions
-  Wipe Save
