# Content Pipeline Extension

[![LDtkMonogame.ContentPipeline](https://img.shields.io/nuget/v/LDtkMonogame.ContentPipeline?label=LDtkMonogame.ContentPipeline) ](https://www.nuget.org/packages/LDtkMonogame.ContentPipeline/)

!> **Please make sure you have the `mgcb-editor` at minimum version `3.8.4`**

This is going to be a bit weird but hang on with me as the content pipeline is really particular.

In your project we are going to need to open a commandline cmd or windows terminal in the folder.
You will then need to navigate to that directory and run these commands.

```shell
dotnet add package LDtkMonogame.ContentPipeline
```

Once that has finished it will have cached the project to the packages directory on your system.
Now you can remove it from your project as your game does not actually need the dll its only used for processing the files.

```sh
dotnet remove package LDtkMonogame.ContentPipeline
```

We have the dll cached to the harddrive you can now go and locate the dll we will be adding to the content pipeline file.
It should be located here.

```sh
C:/Users/<USERNAME>/.nuget/packages/ldtkmonogame.contentpipeline/<VERSION>/lib/net8.0
```

Where `<USERNAME>` is your username on your machine and `<VERSION>` is the latest version of the contentpipeline nuget package as of right now.

Now with that path you can paste it into your content.mgcb here is what i did in mine.

```sh
#-------------------------------- References --------------------------------#

/reference:C:/Users/IrishBruse/.nuget/packages/ldtkmonogame.contentpipeline/1.8.0/lib/net8.0/LDtk.ContentPipeline.dll

#---------------------------------- Content ---------------------------------#
```
