# Codegen

[![LDtkMonogame.Codegen](https://buildstats.info/nuget/LDtkMonogame.Codegen) ](https://www.nuget.org/packages/LDtkMonogame.Codegen/)

LDtk.Codegen package is a code generator that will create c# files for your entities from `.ldtk` files.
This means you can make the variable and set them in ldtk without having to copy the names into a script just so you can use them in your code.
This is very hany when your ldtk file starts to get alot bigger in size.

## Setup

First you need to install the tool which is easy open up cmd/terminal and run

```shell
dotnet tool install --global LDtkMonogame.Codegen
dotnet tool update LDtkMonogame.Codegen --global
```

With that now installed globally you can run it by typing

```shell
ldtkgen
```

It will print a help message to the screen telling you how you can configure ldtkgen

## Automation

So running the tool on every `.ldtk` file you have and remembering the arguments to pass every time would get abit out of hand so you can automate that
put this in your `.csproj` file and set the path to point to your ldtk file(be careful with paths that have a space you will have to escape those in the string).

```xml
<Target Name="Codegen" BeforeTargets="BeforeBuild">
    <Exec Command="ldtkgen -i Content/World.ldtk" />
</Target>
```

## ldtkgen flags

- -i, --input               **Required**. Input LDtk world file.

- -o, --output              **(Default: LDtkTypes/)** The output folder/file depending on if single file is set.

- -n, --namespace           **(Default: LDtkTypes)** Namespace to put the generated files into.

- --LevelClassName          **(Default: LDtkLevelData)** The name to give the custom level file.

- --SingleFile              **(Default: false)** Output all the LDtk files into a single file.

- --PointAsVector2          **(Default: false)** Convert any Point fields or Point[] to Vector2 or Vector2[]

- --FileNameInNamespace     Adds the file name of the world to the namespace eg 'Example.ldtk' will become 'namespace LDtkTypes.Example;'

- --help                    Display the help screen.

- --version                 Display version information.
