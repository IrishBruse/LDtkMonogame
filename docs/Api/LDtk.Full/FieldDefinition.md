# FieldDefinition class

This section is mostly only intended for the LDtk editor app itself. You can safely ignore it.

```csharp
public class FieldDefinition
```

## Public Members

| name | description |
| --- | --- |
| [FieldDefinition](FieldDefinition/FieldDefinition.md)() | The default constructor. |
| [AcceptFileTypes](FieldDefinition/AcceptFileTypes.md) { get; set; } | Optional list of accepted file extensions for FilePath value type. Includes the dot: `.ext` |
| [AllowedRefs](FieldDefinition/AllowedRefs.md) { get; set; } | Possible values: `Any`, `OnlySame`, `OnlyTags`, `OnlySpecificEntity` |
| [AllowedRefsEntityUid](FieldDefinition/AllowedRefsEntityUid.md) { get; set; } | AllowedRefsEntityUid |
| [AllowedRefTags](FieldDefinition/AllowedRefTags.md) { get; set; } | AllowedRefTags |
| [AllowOutOfLevelRef](FieldDefinition/AllowOutOfLevelRef.md) { get; set; } | AllowOutOfLevelRef |
| [ArrayMaxLength](FieldDefinition/ArrayMaxLength.md) { get; set; } | Array max length |
| [ArrayMinLength](FieldDefinition/ArrayMinLength.md) { get; set; } | Array min length |
| [AutoChainRef](FieldDefinition/AutoChainRef.md) { get; set; } | AutoChainRef |
| [CanBeNull](FieldDefinition/CanBeNull.md) { get; set; } | TRUE if the value can be null. For arrays, TRUE means it can contain null values (exception: array of Points can't have null values). |
| [DefaultOverride](FieldDefinition/DefaultOverride.md) { get; set; } | Default value if selected value is null or invalid. |
| [Doc](FieldDefinition/Doc.md) { get; set; } | User defined documentation for this field to provide help/tips to level designers about accepted values. |
| [EditorAlwaysShow](FieldDefinition/EditorAlwaysShow.md) { get; set; } | EditorAlwaysShow |
| [EditorCutLongValues](FieldDefinition/EditorCutLongValues.md) { get; set; } | EditorCutLongValues |
| [EditorDisplayColor](FieldDefinition/EditorDisplayColor.md) { get; set; } | EditorDisplayColor |
| [EditorDisplayMode](FieldDefinition/EditorDisplayMode.md) { get; set; } | Possible values: `Hidden`, `ValueOnly`, `NameAndValue`, `EntityTile`, `LevelTile`, `Points`, `PointStar`, `PointPath`, `PointPathLoop`, `RadiusPx`, `RadiusGrid`, `ArrayCountWithLabel`, `ArrayCountNoLabel`, `RefLinkBetweenPivots`, `RefLinkBetweenCenters` |
| [EditorDisplayPos](FieldDefinition/EditorDisplayPos.md) { get; set; } | Possible values: `Above`, `Center`, `Beneath` |
| [EditorDisplayScale](FieldDefinition/EditorDisplayScale.md) { get; set; } | EditorDisplayScale |
| [EditorLinkStyle](FieldDefinition/EditorLinkStyle.md) { get; set; } | Possible values: `ZigZag`, `StraightArrow`, `CurvedArrow`, `ArrowsLine`, `DashedLine` |
| [EditorShowInWorld](FieldDefinition/EditorShowInWorld.md) { get; set; } | EditorShowInWorld |
| [EditorTextPrefix](FieldDefinition/EditorTextPrefix.md) { get; set; } | EditorTextPrefix |
| [EditorTextSuffix](FieldDefinition/EditorTextSuffix.md) { get; set; } | EditorTextSuffix |
| [ExportToToc](FieldDefinition/ExportToToc.md) { get; set; } | If TRUE, the field value will be exported to the `toc` project JSON field. Only applies to Entity fields. |
| [Identifier](FieldDefinition/Identifier.md) { get; set; } | User defined unique identifier |
| [IsArray](FieldDefinition/IsArray.md) { get; set; } | TRUE if the value is an array of multiple values |
| [Max](FieldDefinition/Max.md) { get; set; } | Max limit for value, if applicable |
| [Min](FieldDefinition/Min.md) { get; set; } | Min limit for value, if applicable |
| [Regex](FieldDefinition/Regex.md) { get; set; } | Optional regular expression that needs to be matched to accept values. Expected format: `/some_reg_ex/g`, with optional "i" flag. |
| [Searchable](FieldDefinition/Searchable.md) { get; set; } | If enabled, this field will be searchable through LDtk command palette |
| [SymmetricalRef](FieldDefinition/SymmetricalRef.md) { get; set; } | SymmetricalRef |
| [TextLanguageMode](FieldDefinition/TextLanguageMode.md) { get; set; } | Possible values: &lt;`null`&gt;, `LangPython`, `LangRuby`, `LangJS`, `LangLua`, `LangC`, `LangHaxe`, `LangMarkdown`, `LangJson`, `LangXml`, `LangLog` |
| [TilesetUid](FieldDefinition/TilesetUid.md) { get; set; } | UID of the tileset used for a Tile |
| [Type](FieldDefinition/Type.md) { get; set; } | Internal enum representing the possible field types. Possible values: F_Int, F_Float, F_String, F_Text, F_Bool, F_Color, F_Enum(...), F_Point, F_Path, F_EntityRef, F_Tile |
| [Uid](FieldDefinition/Uid.md) { get; set; } | Unique Int identifier |
| [UseForSmartColor](FieldDefinition/UseForSmartColor.md) { get; set; } | If TRUE, the color associated with this field will override the Entity or Level default color in the editor UI. For Enum fields, this would be the color associated to their values. |
| [_Type](FieldDefinition/_Type.md) { get; set; } | Human readable value type. Possible values: `Int, Float, String, Bool, Color, ExternEnum.XXX, LocalEnum.XXX, Point, FilePath`. If the field is an array, this field will look like `Array<...>` (eg. `Array<Int>`, `Array<Point>` etc.) NOTE: if you enable the advanced option Use Multilines type, you will have "*Multilines*" instead of "*String*" when relevant. |

## See Also

* namespace [LDtk.Full](../LDtkMonogame.md)

<!-- DO NOT EDIT: generated by xmldocmd for LDtkMonogame.dll -->
