# LevelBackgroundPosition

  
Level background image position info  


## Properties

  
An array of 4 float values describing the cropped sub-rectangle of the displayed  
background image. This cropping happens when original is larger than the level bounds.  
Array format: [ cropX, cropY, cropWidth, cropHeight ]  


```csharp
public float[] CropRect { get; set; }
```

  
An array containing the [scaleX,scaleY] values of the cropped background image,  
depending on bgPos option.  


```csharp
public Vector2 Scale { get; set; }
```

  
An array containing the [x,y] pixel coordinates of the top-left corner of the  
cropped background image, depending on bgPos option.  


```csharp
public Point TopLeftPx { get; set; }
```


