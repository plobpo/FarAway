using Godot;
using System;

public partial class Executioner : RigidBody3D
{
	private readonly double _maxSpeed = 50.0;
	private readonly double _acceleration = 0.6;
	private Vector3 _velocity;

	private double _speed = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_velocity = Vector3.Zero;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsAnythingPressed())
        {
            HandleThrottle(delta);
        }
	}

	private void HandleThrottle(double delta)
	{
		if(Godot.Input.IsActionPressed("throttle_up"))
		{
			_speed = Mathf.Lerp(_speed, _maxSpeed, _acceleration * delta);
		} 
		else if(Godot.Input.IsActionPressed("throttle_down"))
		{
			_speed = Mathf.Lerp(_speed, 0, _acceleration * delta);
		}
	}
}
