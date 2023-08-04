using System.Threading.Tasks;
using Godot;

namespace FarAway.Scripts.core.Network;

public partial class CommController : Node
{
	private Task _orleanProcess;
    public override async void _Ready()
	{
		_orleanProcess = Task.Run(() => SiloController.StartSiloAsync());
		await _orleanProcess;
		GD.Print(_orleanProcess.Status); 
	}
}