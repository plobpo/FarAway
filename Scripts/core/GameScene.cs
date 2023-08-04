using Godot;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

public partial class GameScene : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// private async Task StartSilo()
	// {
	// 	IHostBuilder builder = Host.CreateDefaultBuilder()
    // 	.UseOrleans(silo =>
    // 	{
    //     	silo.UseLocalhostClustering()
    //         	.ConfigureLogging(logging => logging.AddConsole());
    // 	})
    // 	.UseConsoleLifetime();

	// 	using IHost host = builder.Build();

	// 	await host.RunAsync();
	// }
}
