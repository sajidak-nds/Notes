using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utils
{
	public static class FileUtils
	{
		public static void DumpDictionary(Dictionary<string, string> KeyVals, string OutFileF)
		{
			StringBuilder lsbDump = new StringBuilder();

			lsbDump.AppendFormat("{0} \t {1}", "Key", "Value");
			foreach (var vKey in KeyVals.Keys)
			{
				lsbDump.AppendFormat("{0} \t {1}", vKey, KeyVals[vKey]);
				lsbDump.AppendLine("");
			}
			File.WriteAllText(OutFileF, lsbDump.ToString());
		}

		public static void DumpList(List<string> StringList, string OutFileF)
		{
			StringBuilder lsbDump = new StringBuilder();
			StringList.ForEach(x => { lsbDump.AppendLine(x); });
			File.WriteAllText(OutFileF, lsbDump.ToString());
		}

		public static void DumpFile(string TextData, string OutFileF)
		{
			File.WriteAllText(OutFileF, TextData);
		}
	}
}
