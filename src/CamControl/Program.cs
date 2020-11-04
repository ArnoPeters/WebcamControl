using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CamControl.UnitTests;
using CommandLine;
using MFCaptureD3D;

namespace CamControl
{
	//https://github.com/commandlineparser/commandline
	public class Program
	{
		[Verb("List", HelpText = "List all devices.")]
		public class ListOptions
		{
			[Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
			public bool Verbose { get; set; }
		}

		[Verb("Load", HelpText = "Load device settings")]
		public class LoadOptions
		{

	


			[Option('f', "FileName", Required = true, HelpText = "The filename for loading settings")]
			public string FileName { get; set; }
		}

		[Verb("Save", HelpText = "Save device settings")]
		public class SaveOptions
		{
			[Option('f', "FileName", Required = true, HelpText = "The filename for storing settings")]
			public string FileName { get; set; }

			[Option('d', "DeviceName", Required = false, HelpText = "The device name to store settings for. All devices will be listed if left blank")]
			public string DeviceName { get; set; }
		}

	


		static int Main(string[] args)
		{
			

			return CommandLine.Parser.Default.ParseArguments<ListOptions, LoadOptions, SaveOptions>(args)
				.MapResult(
					(ListOptions opts) => Storage.List(opts),
					(LoadOptions opts) => Storage.Load(opts),
					(SaveOptions opts) => Storage.Save(opts),
					errs => 1);


		


			////string fileName = @"d:\camprefs.txt";
			//string fileName = @"d:\logitech_brio_casting.json";
			////await Storage.Save(settingsSet, fileName);
			//string loadfileName = fileName;
			////string loadfileName = @"d:\logitech_brio_casting.json";
			//var loadedSettings = await Storage.Load(loadfileName);

			//VidCapDevices.ApplySettings(loadedSettings);

		}

	}
	public class Storage
	{


		private static JsonSerializerOptions options = new JsonSerializerOptions()
		                                               {
			                                               WriteIndented = true,
			                                               PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			                                               AllowTrailingCommas = true,
			                                               Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
		                                               };

		private static void SaveFile(IEnumerable<Settings> settings, string fileName)
		{
			var jsonString = JsonSerializer.Serialize(settings, options);
			File.WriteAllText(fileName, jsonString);
		}

		private static IReadOnlyList<Settings> LoadFile(string fileName)
		{
			var jsonString = File.ReadAllText(fileName);
			return JsonSerializer.Deserialize<List<Settings>>(jsonString, options);
		}

		public static int List(Program.ListOptions opts)
		{
			foreach (MFDevice mfDevice in VidCapDevices.List())
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(mfDevice.Name);
				if (opts.Verbose)
				{
					sb.Append(" - ").Append(mfDevice.SymbolicName);
				}
				Console.WriteLine(sb.ToString());
			}
			
			return 0;
		}

		public static int Load(Program.LoadOptions opts)
		{
			//todo: validate etc. Try, catch, stuff.
			var s = LoadFile(opts.FileName);
			VidCapDevices.ApplySettings(s);
			return 0;
		}

		public static int Save(Program.SaveOptions opts)
		{
			//todo: validate etc. Try, catch, stuff.
			var list = VidCapDevices.List().Where(d => opts.DeviceName == null || string.Equals(d.Name, opts.DeviceName, StringComparison.Ordinal)).ToArray();
			var l2 = VidCapDevices.GetSettings(list);
			
			SaveFile(l2, opts.FileName);
			return 0;
		}
	}
}
