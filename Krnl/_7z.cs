using System;
using System.Reflection;
using System.Windows;

namespace Krnl
{
	// Token: 0x02000002 RID: 2
	internal class _7z
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static void ExtractArchive(string sourceArchive, string destination)
		{
			Type type = Assembly.LoadFile(MainWindow.DataDir + "\\7z.NET.dll").GetType("SevenZipNET.SevenZipExtractor");
			object obj = Activator.CreateInstance(type, new object[]
			{
				sourceArchive
			});
			try
			{
				type.GetMethod("ExtractAll").Invoke(obj, new object[]
				{
					destination,
					true,
					true
				});
			}
			catch (Exception)
			{
				MessageBox.Show("Failed to extract an archive file.");
			}
			Console.ReadLine();
		}
	}
}
