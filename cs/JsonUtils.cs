using System;
using J = Newtonsoft.Json;

namespace Utils
{
	public static class JsonUtils
	{
		public static Newtonsoft.Json.JsonSerializerSettings JsonSettingsDefault
		{
			get
			{
				return new Newtonsoft.Json.JsonSerializerSettings()
				{
					Formatting = Newtonsoft.Json.Formatting.None,
					NullValueHandling = Newtonsoft.Json.NullValueHandling.Include,
					MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore,
					StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeNonAscii,
					//ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
				};
			}
		}

		public static string ToJSON(Object obj, bool pPrettyPrint = true)
		{
			if (obj == null)
			{
				Console.WriteLine("Cannot serialize a Null object");
				return string.Empty;
			}
			string lsJSON = string.Empty;
			Newtonsoft.Json.JsonSerializerSettings loSettings = JsonSettingsDefault;
			loSettings.Formatting =
						pPrettyPrint
						? Newtonsoft.Json.Formatting.Indented
						: Newtonsoft.Json.Formatting.None
						;
			try
			{
				lsJSON = J.JsonConvert.SerializeObject(obj, loSettings);
			}
			catch (Newtonsoft.Json.JsonSerializationException ExSEX)
			{
				Console.WriteLine("Parsing Error, Trying with additional settings.");
				Console.WriteLine(ExSEX.Message);
				loSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
				lsJSON = ToJSON(obj, loSettings);
			}
			catch (System.Exception ExSE)
			{
				Console.WriteLine("Unexpected error while parsing JSON");
				Console.WriteLine(ExSE.Message);
			}
			return lsJSON;
		}

		public static string ToJSON(Object obj, Newtonsoft.Json.JsonSerializerSettings pSettings)
		{
			if (obj == null)
			{
				Console.WriteLine("Cannot serialize a Null object");
				return string.Empty;
			}
			string lsJSON = string.Empty;
			Newtonsoft.Json.JsonSerializerSettings loSettings = pSettings ?? new J.JsonSerializerSettings();
			try
			{
				lsJSON = J.JsonConvert.SerializeObject(obj, loSettings);
			}
			catch (System.Exception ExSE)
			{
				Console.WriteLine("Unexpected error while parsing JSON");
				Console.WriteLine(ExSE.Message);
			}
			return lsJSON;
		}

		public static object FromJSON(string psJsonText, Type pType)
		{
			object dsObj = null;
			if (string.IsNullOrWhiteSpace(psJsonText))
			{
				Console.WriteLine("Input string is not proper. {0}", psJsonText);
				return dsObj;
			}
			if (pType == null)
			{
				Console.WriteLine("Cannot convert without type information");
			}

			// log.Debug("Converting to object Type {0}", pType.FullName);
			try
			{
				dsObj = J.JsonConvert.DeserializeObject(psJsonText, pType);
				//Console.WriteLine("Conversion is Successful, to object Type {0}", dsObj.GetType().FullName);
			}
			catch (Exception eX)
			{
				Console.WriteLine("Unexpected error on deserializing Json string to Object");
				Console.WriteLine(eX.Message);
			}
			return dsObj;
		}

	}
}
