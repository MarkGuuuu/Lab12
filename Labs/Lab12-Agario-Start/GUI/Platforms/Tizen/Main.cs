using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Lab12_Agario_Start_JSON_Graphics;

class Program : MauiApplication
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	static void Main(string[] args)
	{
		var app = new Program();
		app.Run(args);
	}
}
