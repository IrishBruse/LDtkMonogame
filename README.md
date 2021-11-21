<p align="center">
  <a href="https://github.com/deepnight/ldtk"> <img alt="LDtk Version Support" src="https://img.shields.io/github/v/release/deepnight/ldtk?&label=LDtk&color=yellow"></a>
  <a href="https://www.nuget.org/packages/LDtkMonogame/"><img src="https://img.shields.io/nuget/v/LDtkMonogame?" /></a>
  <a href="https://www.nuget.org/packages/LDtkMonogame/"><img alt="Nuget" src="https://img.shields.io/nuget/dt/LDtkMonogame"></a>
</p>
<p align="center">
  <a href="https://github.com/IrishBruse/LDtkMonogame/tree/main/LDtkMonogame"> <img alt="GitHub Package Build Status" src="https://img.shields.io/github/workflow/status/IrishBruse/LDtkMonogame/Build%20Package?label=LDtkMonogame"></a>
  <a href="https://github.com/IrishBruse/LDtkMonogame/tree/main/LDtkMonogame.Examples"> <img alt="GitHub Examples Build Status" src="https://img.shields.io/github/workflow/status/IrishBruse/LDtkMonogame/Build%20Examples?label=LDtkMonogame.Examples"></a>
</p>

---

<h1 align="center">
    <a href="https://irishbruse.github.io/LDtkMonogame/">LDtkMonogame Wiki</a>
</h1>

## Build instructions

This is build instructions for the LDtk library not for your game.

Game instructions are on the wiki link above.

`LDtk.Codegen` & `LDtk.ContentPipeline` need to be built before the examples
as the examples depend on them.

You can just run `build.bat`
it will build everything in the correct order and from then on unless
you make changes to `LDtk.Codegen` or `LDtk.ContentPipeline` building like normal should work

Ignore any error produced from the build.bat
