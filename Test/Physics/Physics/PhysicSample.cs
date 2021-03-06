﻿using DigitalRune.Geometry;
using DigitalRune.Geometry.Collisions;
using DigitalRune.Geometry.Shapes;
using DigitalRune.Mathematics.Algebra;
using DigitalRune.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace DigitalRune.Samples.Physics
{
  // The PhysicsSample represents a base class for physics samples. In Update()
  // it draws all rigid bodies of the physics simulation and debug information using
  // the DebugRenderer.
  [Controls(@"Debug Rendering of Physics
  Press <C>, <B>, <I> to render Contacts, Bounding Boxes or Simulation Islands.
  Press <L> to render sleeping bodies in a different color.
  Press <M> to toggle wire frame mode")]
  public abstract class PhysicsSample : BasicSample
  {
    private readonly GrabObject _grabObject;
    private readonly BallShooterObject _ballShooterObject;
    private readonly ExplosionObject _explosionObject;
    private bool _drawWireFrame;
    private bool _showSleeping;
    private bool _drawContacts;
    private bool _drawBoundingBoxes;
    private bool _drawIslands;


    protected PhysicsSample(Microsoft.Xna.Framework.Game game)
      : base(game)
    {
      GraphicsScreen.ClearBackground = true;
      GraphicsScreen.BackgroundColor = Color.White;
      GraphicsScreen.DrawReticle = true;
      SetCamera(new Vector3F(0, 2, 10), 0, 0);

      // Add game objects which implement simple physics interactions.
      _grabObject = new GrabObject(Services);
      _ballShooterObject = new BallShooterObject(Services);
      _explosionObject = new ExplosionObject(Services);
      GameObjectService.Objects.Add(_grabObject);
      GameObjectService.Objects.Add(_ballShooterObject);
      GameObjectService.Objects.Add(_explosionObject);
    }


    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        // Remove game objects.
        GameObjectService.Objects.Remove(_grabObject);
        GameObjectService.Objects.Remove(_ballShooterObject);
        GameObjectService.Objects.Remove(_explosionObject);
      }

      base.Dispose(disposing);
    }


    public override void Update(GameTime gameTime)
    {
      // Toggle between wireframe and normal mode if <M> is pressed.
      if (InputService.IsPressed(Keys.M, true))
        _drawWireFrame = !_drawWireFrame;

      // If <L> is pressed render the sleeping (inactive) bodies in a different color.
      if (InputService.IsPressed(Keys.L, true))
        _showSleeping = !_showSleeping;

      // Visualize contacts if <C> is pressed.
      if (InputService.IsPressed(Keys.C, true))
        _drawContacts = !_drawContacts;

      // When contact drawing is enabled, we must make sure that the contact information
      // is up-to-date after Simulation.Update().
      Simulation.Settings.SynchronizeCollisionDomain = _drawContacts;

      // Visualize axis-aligned bounding boxes if <B> is pressed.
      if (InputService.IsPressed(Keys.B, true))
        _drawBoundingBoxes = !_drawBoundingBoxes;

      // Visualize simulation islands if <I> is pressed.
      if (InputService.IsPressed(Keys.I, true))
        _drawIslands = !_drawIslands;

      // ----- Draw rigid bodies using the DebugRenderer of the graphics screen.
      var debugRenderer = GraphicsScreen.DebugRenderer;
      debugRenderer.Clear();
      foreach (var body in Simulation.RigidBodies)
      {
        // To skip automatic drawing of bodies, the sub-classes can set the UserData
        // property to "NoDraw".
        if (body.UserData is string && (string)body.UserData == "NoDraw")
          continue;

        var color = Color.Gray;
        // Draw static and, optionally, sleeping bodies with different colors.
        if (body.MotionType == MotionType.Static || _showSleeping && body.IsSleeping)
          color = Color.LightGray;

        debugRenderer.DrawObject(body, color, _drawWireFrame, false);
      }

      // Draw contacts.
      if (_drawContacts)
        debugRenderer.DrawContacts(Simulation.CollisionDomain.ContactSets, 0.1f, Color.DarkOrange, true);

      // Draw AABBs.
      if (_drawBoundingBoxes)
      {
        foreach (CollisionObject collisionObject in Simulation.CollisionDomain.CollisionObjects)
        {
          if (collisionObject.Enabled)
            debugRenderer.DrawAabb(collisionObject.GeometricObject.Aabb, Pose.Identity, new Color(0x80, 0, 1), false);
        }
      }

      // Draw simulation islands.
      if (_drawIslands)
        DrawIslands();
    }


    // Visualizes the simulation islands of a simulation.
    private void DrawIslands()
    {
      // Loop over all simulation islands. Draw AABBs of islands.
      var debugRenderer = GraphicsScreen.DebugRenderer;
      foreach (var island in Simulation.IslandManager.Islands)
      {
        // Compute AABB of island.
        Aabb aabb = island.RigidBodies[0].Aabb;
        foreach (var body in island.RigidBodies)
          aabb.Grow(body.Aabb);

        debugRenderer.DrawAabb(aabb, Pose.Identity, Color.Yellow, false);
      }
    }
  }
}
