using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CamControl.UnitTests;
using MFCaptureD3D;

namespace CamControl
{
	public class Storage
	{
		private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
		                                                        {
			                                                        WriteIndented = true,
			                                                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			                                                        AllowTrailingCommas = true,
			                                                        Converters = {new JsonStringEnumConverter()}
		                                                        };

		public static void SaveFile(IEnumerable<Settings> settings, string fileName)
		{
			string jsonString = JsonSerializer.Serialize(settings, Options);
			File.WriteAllText(fileName, jsonString);
		}

		public static IReadOnlyList<Settings> LoadFile(string fileName)
		{
			string jsonString = File.ReadAllText(fileName);
			return JsonSerializer.Deserialize<List<Settings>>(jsonString, Options);
		}
	}
}