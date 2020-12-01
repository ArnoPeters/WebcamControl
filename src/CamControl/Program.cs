using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CamControl.UnitTests;
using CommandLine;
using MFCaptureD3D;

namespace CamControl
{
	//https://github.com/commandlineparser/commandline
	public class Program
	{
		private static int Main(string[] args)
		{
			return Parser.Default.ParseArguments<ListOptions, LoadOptions, SaveOptions>(args)
				.MapResult(
					(ListOptions opts) => List(opts),
					(LoadOptions opts) => Load(opts), 
					(SaveOptions opts) => Save(opts), 
					errs => 1);
		}

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

		public static int List(Program.ListOptions opts)
		{
			foreach (MFDevice mfDevice in VidCapDevices.List())
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(mfDevice.Name);
				if (opts.Verbose) { sb.Append(" - ").Append(mfDevice.SymbolicName); }
				Console.WriteLine(sb.ToString());
			}

			return 0;
		}

		public static int Load(Program.LoadOptions opts)
		{
			//todo: validate etc. Try, catch, stuff.
			IReadOnlyList<Settings> s = Storage.LoadFile(opts.FileName);
			VidCapDevices.ApplySettings(s);
			return 0;
		}

		public static int Save(Program.SaveOptions opts)
		{
			//todo: validate etc. Try, catch, stuff.
			MFDevice[] list = VidCapDevices.List().Where(d => opts.DeviceName == null || string.Equals(d.Name, opts.DeviceName, StringComparison.Ordinal)).ToArray();
			IEnumerable<Settings> l2 = VidCapDevices.GetSettings(list);

			Storage.SaveFile(l2, opts.FileName);
			return 0;
		}
	}
}