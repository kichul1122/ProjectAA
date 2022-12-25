using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public static class AAMenuItem
{
	[MenuItem("AA/CodeGenerate")]
	private static void Generate()
	{
		ExecuteMasterMemoryCodeGenerator();
		ExecuteMessagePackCodeGenerator();
	}

	private static void ExecuteMasterMemoryCodeGenerator()
	{
		UnityEngine.Debug.Log($"{nameof(ExecuteMasterMemoryCodeGenerator)} : start");

		var exProcess = new Process();

		var filePath = $"{Application.dataPath}/_Batch/MasterMemoryGenerator.bat";

		var psi = new ProcessStartInfo()
		{
			CreateNoWindow = true,
			WindowStyle = ProcessWindowStyle.Hidden,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			UseShellExecute = false,
			FileName = filePath,
			//Arguments = $@"-i ""{Application.dataPath}/Scripts/Tables"" -o ""{Application.dataPath}/Scripts/Generated"" -n ""MasterData""",
		};

		var p = Process.Start(psi);

		p.EnableRaisingEvents = true;
		p.Exited += (object sender, System.EventArgs e) =>
		{
			var data = p.StandardOutput.ReadToEnd();
			UnityEngine.Debug.Log($"{data}");
			UnityEngine.Debug.Log($"{nameof(ExecuteMasterMemoryCodeGenerator)} : end");
			p.Dispose();
			p = null;
		};
	}

	private static void ExecuteMessagePackCodeGenerator()
	{
		UnityEngine.Debug.Log($"{nameof(ExecuteMessagePackCodeGenerator)} : start");

		var exProcess = new Process();

		var filePath = $"{Application.dataPath}/_Batch/MasterMemoryGenerator.bat";

		var psi = new ProcessStartInfo()
		{
			CreateNoWindow = true,
			WindowStyle = ProcessWindowStyle.Hidden,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			UseShellExecute = false,
			FileName = filePath,
			//Arguments = $@"-i ""{Application.dataPath}/../Assembly-CSharp.csproj"" -o ""{Application.dataPath}/Scripts/Generated/MessagePack.Generated.cs""",
		};

		var p = Process.Start(psi);

		p.EnableRaisingEvents = true;
		p.Exited += (object sender, System.EventArgs e) =>
		{
			var data = p.StandardOutput.ReadToEnd();
			UnityEngine.Debug.Log($"{data}");
			UnityEngine.Debug.Log($"{nameof(ExecuteMessagePackCodeGenerator)} : end");
			p.Dispose();
			p = null;
		};
	}
}