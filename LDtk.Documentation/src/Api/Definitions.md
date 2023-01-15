# Definitions

  
If you're writing your own LDtk importer, you should probably just ignore most stuff in  
the defs section, as it contains data that are mostly important to the editor. To keep  
you away from the defs section and avoid some unnecessary JSON parsing, important data  
from definitions is often duplicated in fields prefixed with a float underscore (eg.  
__identifier or __type).  The 2 only definition types you might  
need here are Tilesets and Enums.  
A structure containing all the definitions of this project  


## Properties

- types **LDtk.Definitions.Entities**
- types **LDtk.Definitions.Enums**
- types **LDtk.Definitions.ExternalEnums**
- types **LDtk.Definitions.Layers**
- types **LDtk.Definitions.LevelFields**
- types **LDtk.Definitions.Tilesets**

