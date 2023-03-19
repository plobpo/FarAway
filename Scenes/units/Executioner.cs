using Godot;
using System;

public partial class Executioner : RigidBody3D
{
	private readonly double _maxSpeed = 50.0;
	private readonly double _acceleration = 0.5;
	private float _pitchSpeed = 0.05f;
	private float _rollSpeed = 0.9f;
	private float _yawSpeed = 0.05f;
	private Vector3 _velocity;

	private Vector3 _rotation;

	private double _speed = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_velocity = Vector3.Zero;
		_rotation = Vector3.Zero;
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	// public override void  OnInputEvent(InputEvent inputEvent)
	// {
	// 	GD.Print("WoooHooo!");
	// }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsAnythingPressed())
        {
            HandleThrottleOrRoll(delta);
        }
		
		if(Input.GetLastMouseVelocity() != Vector2.Zero)
		{
			HandleMouse(delta);
		}
	}

	private void HandleMouse(double delta)
	{
		/*
		Yaw is the rotation of an object around the vertical axis, 
		which is commonly referred to as the yaw axis. 
		Pitch, on the other hand, is the rotation of an object around the horizontal axis, 
		which is commonly referred to as the pitch axis.
		*/
		Vector2 mouseMotion = Input.GetLastMouseVelocity();
		_rotation.Y = -mouseMotion.X * _pitchSpeed * (float)delta;
		_rotation.X = -mouseMotion.Y * _yawSpeed * (float)delta;
	}

	private void HandleThrottleOrRoll(double delta)
	{
		if(Godot.Input.IsActionPressed("throttle_up"))
		{
			_speed = Mathf.Lerp(_speed, _maxSpeed, _acceleration * delta);
		} 
		else if(Godot.Input.IsActionPressed("throttle_down"))
		{
			_speed = Mathf.Lerp(_speed, 0, _acceleration * delta);
		}
		
		if(Input.IsActionPressed("roll_right"))
		{
			var calculatedZRotation = GetRotation().Z + (_rollSpeed * (float)delta * -1);
			_rotation.Z = calculatedZRotation; 			

		} else if (Input.IsActionPressed("roll_left"))
		{
			var calculatedZRotation = GetRotation().Z + (_rollSpeed * (float)delta);
			_rotation.Z = calculatedZRotation; 	
		}
	}
    public override void _PhysicsProcess(double delta)
    {
		_velocity = Transform.Basis.Z * (float)_speed;
		if(_rotation != Vector3.Zero)
		{
			// information about node location in the world
			Vector3 center = GlobalTransform.Origin;
			/*
			Here's a breakdown of what's happening in this code:

GlobalTransform.Origin retrieves the world space position of the node.
The negation -GlobalTransform.Origin creates a vector that points from the world space origin to the node's position. This is because subtracting a vector from the origin gives you a vector that points to that position.
Transform.Translated creates a new Transform object that represents a translation from one point in space to another. In this case, the translation is from the node's local space to the world space origin.
The resulting Transform object can be used to transform other points or nodes from the node's local space to the world space origin.
			*/
			Transform3D translatedTransform = Transform.Translated(-center);
			Transform3D rotatedTransform = translatedTransform.Rotated(_rotation.Normalized(), Mathf.DegToRad(_rotation.Length()));
			Transform = rotatedTransform.Translated(center);
			
		}
		
		MoveAndCollide(_velocity * (float)delta);
    }

	private Vector3 GetRotation()
	{
		if(_rotation == Vector3.Zero)
		{
			_rotation = new Vector3(0, 0, 0);
		}
		return _rotation;
	}
}
