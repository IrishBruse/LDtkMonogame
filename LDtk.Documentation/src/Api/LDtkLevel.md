# LDtkLevel

  
This section contains all the level data. It can be found in 2 distinct forms, depending  
on Project current settings:  - If "Separate level files" is disabled (default):  
full level data is embedded inside the main Project JSON file, - If "Separate level  
files" is enabled: level data is stored in separate standalone .ldtkl files (one  
per level). In this case, the main Project JSON file will still contain most level data,  
except heavy sections, like the layerInstances array (which will be null). The  
externalRelPath string points to the ldtkl file.  A ldtkl file is just a JSON file  
containing exactly what is described below.  


## Methods

- types **LDtk.LDtkLevel.#ctor**
- types **LDtk.LDtkLevel.FromFile**
- types **LDtk.LDtkLevel.FromFile**
- types **LDtk.LDtkLevel.GetIntGrid**
- types **LDtk.LDtkLevel.GetCustomFields``1**
- types **LDtk.LDtkLevel.GetEntity``1**
- types **LDtk.LDtkLevel.GetEntityRef``1**
- types **LDtk.LDtkLevel.GetEntities``1**
- types **LDtk.LDtkLevel.Contains**
- types **LDtk.LDtkLevel.Contains**

## Properties

- types **LDtk.LDtkLevel.FilePath**
- types **LDtk.LDtkLevel.WorldFilePath**
- types **LDtk.LDtkLevel.Position**
- types **LDtk.LDtkLevel.Size**
- types **LDtk.LDtkLevel.Loaded**
- types **LDtk.LDtkLevel._BgColor**
- types **LDtk.LDtkLevel._BgPos**
- types **LDtk.LDtkLevel._Neighbours**
- types **LDtk.LDtkLevel.BgRelPath**
- types **LDtk.LDtkLevel.ExternalRelPath**
- types **LDtk.LDtkLevel.FieldInstances**
- types **LDtk.LDtkLevel.Identifier**
- types **LDtk.LDtkLevel.Iid**
- types **LDtk.LDtkLevel.LayerInstances**
- types **LDtk.LDtkLevel.PxHei**
- types **LDtk.LDtkLevel.PxWid**
- types **LDtk.LDtkLevel.Uid**
- types **LDtk.LDtkLevel.WorldDepth**
- types **LDtk.LDtkLevel.WorldX**
- types **LDtk.LDtkLevel.WorldY**

