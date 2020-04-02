
# Purpose

This mod eliminates the "energy" cost for Refining, Smelting, and Smithing so these actions can be taken as long as materials are available.

# Installation

Once built, the SmithForever directory can be copied to the Bannerlord module directory (`C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules`).

# Development

Until TaleWorlds releases mod tools, development is a bit hacky and involves inspection of decompiled code.

Some tools:

* [Visual Studio Community Edition](https://visualstudio.microsoft.com/)
* [JetBrains dotPeek](https://www.jetbrains.com/decompiler/)

Workflows that may prove useful:

* Adding all of the DLLs in the game directory as references makes searching the entire code base possible (`C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.*.dll`)
* Once a promising class is found, the 'source' can be recovered by opening the DLL in dotPeek

Where to start:

* For single-player, lots of logic of interest is compiled in `TaleWorlds.CampaignSystem.dll`
* Specifically, `TaleWorlds.CampaignSystem.Sandbox.GameComponents` and `TaleWorlds.CampaignSystem.Sandbox.GameComponents.Map` contain lots of classes that permit changing balance.

The basic concept:

* TaleWorlds has structured most game code using abstract classes and default implementations
* One example of this paradigm is the `SmithingModel` and `DefaultSmithingModel`, respectively
* Standard inheritance + override development flow allows changing game behavior

Loading code:

* To hook a module into the game a SubModule class is specified in `SubModule.xml`
* The SubModule class is responsible for loading all other classes
* See `MBSubModuleBase` for all of the hooks available for loading classes
* See `SandBoxManager` for an example of concrete code for loading classes

Loading order and override:

* The game maintains a big list of models and this is searched when inserting new models
* Load order is determined by module depencies
* If dependencie are accurate, loading models is automagic
  * By simply adding a subclass of a default model after the default model the game will find the subclass and overriden behaviors will be used in game
