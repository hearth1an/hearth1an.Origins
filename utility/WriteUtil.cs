using OWML.Common;

namespace Origins.Utility
{
	public static class WriteUtil
	{	
		public static void WriteLine(string line) => WriteLine(line, MessageType.Info);
		public static void WriteError(string line) => WriteLine(line, MessageType.Error);
		public static void WriteLine(string line, MessageType type) => Origins.Instance.ModHelper.Console.WriteLine($"{type}: " + line, type);
	}
}