

var orginalUnit = preferences.rulerUnits;
preferences.rulerUnits = Units.INCHES;

var docRef = app.documents.add(2,4);

var artLayerRef = docRef.artLayers.add();
var groupLayerSetRef = app.activeDocument.layerSets.add();

artLayerRef.kind = LayerKind.TEXT;
artLayerRef.move(groupLayerSetRef, ElementPlacement.INSIDE);

var textItemRef = artLayerRef.textItem;

textItemRef.contents = "Hello world";

var colorRef = new SolidColor;
colorRef.rgb.red = 100;
colorRef.rgb.green = 0;
colorRef.rgb.blue = 0;
textItemRef.color = colorRef;

docRef = null;
artLayerRef = null;
textItemRef = null;
groupLayerSetRef = null;

app.preferences.rulerUnits = orginalUnit;