
# Purpose

This mod eliminates the "energy" cost for Refining, Smelting, and Smithing so these actions can be taken as long as materials are available.

Class overrides:

* SmithingModel

# Installation

Once built, the SmithForever directory can be copied to the Bannerlord module directory (`C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules`).

# Development

Until TaleWorlds releases mod tools, development is a bit hacky and involves inspection of decompiled code.

## Some tools

* [Visual Studio Community Edition](https://visualstudio.microsoft.com/)
* [JetBrains dotPeek](https://www.jetbrains.com/decompiler/)

## Workflows that may prove useful

* Adding all of the DLLs in the game directory as references makes searching the entire code base possible (`C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.*.dll`)
* Once a promising class is found, the 'source' can be recovered by opening the DLL in dotPeek
  * Decompiled code will not have comments and will include compiler-generated code like empty constructors that can be deleted

## Where to start looking

Taleworlds followed a `behavior`-`model` paradigm with the achitecture. This separates the responsibilties of the code more-or-less into mechanism and policy, respectively.

* Behaviors are responsible for doing things, like starting and stopping tournaments or unlocking new crafting parts, for example
* Models are responsible for deciding when, how frequently, how fast, etc. to do things

For light modding of balance and to affect existing behaviors, it is probably best to start with modding models.

* For single-player, lots of logic of interest is compiled in `TaleWorlds.CampaignSystem.dll`
* Specifically, `TaleWorlds.CampaignSystem.Sandbox.GameComponents` and `TaleWorlds.CampaignSystem.Sandbox.GameComponents.Map` contain lots of classes that permit changing balance

To foundationally change what can happen in the game, behaviors must be changed. The behaviors for each game mode are loaded by that module, so the fastest way to get started finding what behaviors to modify may be to look at the submodule class for a game mode.

* For the sandbox campaign the module class is specified as `Sandbox.SandBoxSubModule` in `Modules\SandBox\SubModule.xml` and can be found in `Modules\SandBox\bin\Win64_Shipping_Client\Sandbox.ddl`

## How modules work

### Inheritance

* TaleWorlds has structured most game code using abstract classes and default implementations
* One example of this paradigm is the `SmithingModel` and `DefaultSmithingModel`, respectively
* Standard inheritance + override development flow allows changing game behavior

### Loading code

* To hook a module into the game a SubModule class is specified in `SubModule.xml`
* The SubModule class is responsible for loading all other modded classes
* See `MBSubModuleBase` for all of the hooks available for loading classes
* See `SandBoxManager` for an example of concrete code for loading classes

### Loading order and override

* The game maintains a big list of models and this is searched when inserting new models
* Load order is determined by module depencies
* If dependencies are accurate, loading models is automagic
  * By simply adding a subclass of a default model after the default model the game will find the subclass and overriden behaviors will be used in game

## Mod conflicts

* Class-level override is the finest-grained mod multiplexing the game supports, so any two mods that change the same class must have a module dependency + class inheritance relationship or will conflict
  * An example of a coarser-grained model is `SmithingModel`, where time costs, skill effects, and material requirements are defined in the same model, so a compilation mod or mod dependency is required to mod all of these
  * A example of finer-grained models is hero progression, where age, skill, and renown are handled independently in `AgeModel`, `CharacterDevelopmentModel`, and `ClanTierModel`, respectively
* Given the inconsistent granularity of classes, it is advisable to list the classes overridden in a mod description so users can know definitively what mods conflict
  * This can also aid creation of compilation mods
