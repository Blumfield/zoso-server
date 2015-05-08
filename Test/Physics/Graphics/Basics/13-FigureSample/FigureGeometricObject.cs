﻿using DigitalRune.Geometry;
using DigitalRune.Graphics.SceneGraph;


namespace DigitalRune.Samples.Graphics
{
  // A GeometricObject class which stores a reference to a FigureNode.
  internal class FigureGeometricObject : GeometricObject
  {
    public FigureNode FigureNode { get; set; }
  }
}
