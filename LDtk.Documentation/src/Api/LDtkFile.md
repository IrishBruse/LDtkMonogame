# LDtkFile

  
This file is a JSON schema of files created by LDtk level editor (https://ldtk.io).  
This is the root of any Project JSON file. It contains:  - the project settings, - an  
array of levels, - a group of definitions (that can probably be safely ignored for most  
users).  


## Methods

Used by json deserializer not for use by user!

```csharp
LDtkFile.#ctor
```

Loads the ldtk world file from disk directly

```csharp
public LDtkFile FromFile(string)
```

Loads the ldtk world file from disk directly

```csharp
public LDtkFile FromFile(string,ContentManager)
```

Loads the ldtkl world file from disk directly or from the embeded one depending on if the file uses externalworlds

```csharp
public LDtkWorld LoadWorld(Guid)
```

Gets an entity from an  converted to

```csharp
public T GetEntityRef<T>(EntityRef)
```


## Properties

The absolute path to the ldtkFile

```csharp
public string FilePath { get; set; }
```

The content manager used if you are using the contentpipeline

```csharp
public ContentManager Content { get; set; }
```

  
Project background color  


```csharp
public Color BgColor { get; set; }
```

  
A structure containing all the definitions of this project  


```csharp
public Definitions Defs { get; set; }
```

  
If TRUE, one file will be saved for the project (incl. all its definitions) and one file  
in a sub-folder for each level.  


```csharp
public bool ExternalLevels { get; set; }
```

  
Unique project identifier  


```csharp
public Guid Iid { get; set; }
```

  
File format version  


```csharp
public string JsonVersion { get; set; }
```

  
All instances of entities that have their exportToToc flag enabled are listed in this  
array.  


```csharp
public LdtkTableOfContentEntry[] Toc { get; set; }
```

  
WARNING: this field will move to the worlds array after the "multi-worlds" update.  
It will then be null. You can enable the Multi-worlds advanced project option to enable  
the change immediately. Height of the world grid in pixels.  


```csharp
public int? WorldGridHeight { get; set; }
```

  
WARNING: this field will move to the worlds array after the "multi-worlds" update.  
It will then be null. You can enable the Multi-worlds advanced project option to enable  
the change immediately. Width of the world grid in pixels.  


```csharp
public int? WorldGridWidth { get; set; }
```

  
WARNING: this field will move to the worlds array after the "multi-worlds" update.  
It will then be null. You can enable the Multi-worlds advanced project option to enable  
the change immediately. An enum that describes how levels are organized in  
this project (ie. linearly or in a 2D space). Possible values: <null>, Free,  
GridVania, LinearHorizontal, LinearVertical, null  


```csharp
public WorldLayout? WorldLayout { get; set; }
```

  
This array is not used yet in current LDtk version (so, for now, it's always  
empty).In a later update, it will be possible to have multiple Worlds in a  
single project, each containing multiple Levels.What will change when "Multiple  
worlds" support will be added to LDtk:- in current version, a LDtk project  
file can only contain a single world with multiple levels in it. In this case, levels and  
world layout related settings are stored in the root of the JSON.- after the  
"Multiple worlds" update, there will be a worlds array in root, each world containing  
levels and layout settings. Basically, it's pretty much only about moving the levels  
array to the worlds array, aint with world layout related values (eg. worldGridWidth  
etc).If you want to start supporting this future update easily, please refer to  
this documentation: https://github.com/deepnight/ldtk/issues/231  


```csharp
public LDtkWorld[] Worlds { get; set; }
```


