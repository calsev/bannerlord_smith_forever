
# Purpose

This mod eliminates the "energy" cost for Refining, Smelting, and Smithing so these actions can be taken as long as materials are available.

# Installation

Once built, the SmithForever directory can be copied to the Bannerlord module directory (C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\Modules).

# Development

Until Taleworlds releases mod tools, development is a bit hacky and involves inspection of decompiled code.

Some tools:

* [Visual Studio Community Edition](https://visualstudio.microsoft.com/)
* [JetBrains dotPeek](https://www.jetbrains.com/decompiler/)

Workflows that may prove useful:

* Adding all of the DLLs in the game directory as references makes searching the entire code base possible (C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.*.dll)
* Once a promising class is found, the 'source' can be recovered by opening the DLL in dotPeek

Where to start:

* For single-player, lots of logic of interest is compiled in TaleWorlds.CampaignSystem.dll
