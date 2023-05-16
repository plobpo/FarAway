using Godot;

public partial class TrackingCamera : Camera3D
{
	[Export]
	public NodePath target_path;

	[Export]
	public float Offset = 1.0f;

	[Export]
	public Vector3 Offset_As_Vector = Vector3.Zero;

	private RigidBody3D _target;

	private float _followWeight = 3.0f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(!string.IsNullOrEmpty(target_path))
		{
			_target = GetNode<RigidBody3D>(target_path);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(_target != null)
		{
			//point where camera will be trying to reach :D
			//var targetPosition = _target.GlobalTransform.Translated(Offset_As_Vector);
			var targetPosition = _target.GlobalTransform.Origin + _target.GlobalTransform.Basis * Offset_As_Vector;
			//GlobalTransform = GlobalTransform.InterpolateWith(targetPosition, _followWeight * (float)delta);
			GlobalTransform = new Transform3D(GlobalTransform.Basis, targetPosition);

			// here we are calculating new position with offset so Camera will be following the target
			/*GlobalTransform = new Transform3D(
                GlobalTransform.Basis,
                _target.GlobalTransform.Origin - _target.Transform.Basis.Z * Offset
            );*/

			// camera needs to track the target object
			LookAt(_target.GlobalTransform.Origin, Vector3.Up);
		}
	}
}
