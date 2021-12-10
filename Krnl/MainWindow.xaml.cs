using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Krnl.Properties;

namespace Krnl
{
	// Token: 0x02000004 RID: 4
	public partial class MainWindow : Window
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000211C File Offset: 0x0000031C
		public MainWindow()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021AC File Offset: 0x000003AC
		private void Grid_Loaded(object sender, RoutedEventArgs e)
		{
			Task.Run(delegate()
			{
				this.DisplayText("Getting version..");
				this.DllVersion = this.wc.DownloadString(this.DediUrl + "version.txt");
				this.UiVersion = this.wc.DownloadString(this.StorageUrl + "version.txt");
				this.DisplayText("Checking files..");
				if (!Directory.Exists(this.KrnlDir))
				{
					Directory.CreateDirectory(this.KrnlDir);
				}
				if (!Directory.Exists(MainWindow.DataDir))
				{
					Directory.CreateDirectory(MainWindow.DataDir);
				}
				if (!Directory.Exists(MainWindow.CommunityDir))
				{
					Directory.CreateDirectory(MainWindow.CommunityDir);
				}
				if (!File.Exists(this.ConfigFile))
				{
					File.WriteAllLines(this.ConfigFile, new string[]
					{
						this.DllVersion,
						this.UiVersion
					});
				}
				File.WriteAllBytes(MainWindow.DataDir + "\\7za.exe", Krnl.Properties.Resources._7za);
				File.WriteAllBytes(MainWindow.DataDir + "\\7z.NET.dll", Krnl.Properties.Resources._7z_NET);
				this.DisplayText("Downloading..");
				this.DownloadArchive();
				this.DisplayText("Extracting..");
				Directory.SetCurrentDirectory(MainWindow.DataDir);
				if (File.Exists(this.KrnlDir + "\\krnl.7z"))
				{
					_7z.ExtractArchive(this.KrnlDir + "\\krnl.7z", this.KrnlDir);
				}
				if (File.Exists(MainWindow.DataDir + "\\Community.7z"))
				{
					_7z.ExtractArchive(MainWindow.DataDir + "\\Community.7z", MainWindow.CommunityDir);
				}
				this.DisplayText("Starting..");
				Directory.SetCurrentDirectory(this.KrnlDir);
				if (File.Exists(this.KrnlDir + "\\KrnlUI.exe"))
				{
					Process.Start(this.KrnlDir + "\\KrnlUI.exe");
				}
				if (File.Exists(this.KrnlDir + "\\krnl.7z"))
				{
					File.Delete(this.KrnlDir + "\\krnl.7z");
				}
				base.Dispatcher.Invoke(delegate()
				{
					base.Hide();
				});
				Environment.Exit(-1);
			});
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021C0 File Offset: 0x000003C0
		private void DownloadArchive()
		{
			this.DisplayText("Downloading krnl.dll..");
			if (this.ReadConfig(0) != this.DllVersion || !File.Exists(this.KrnlDir + "\\krnl.dll"))
			{
				this.wc.DownloadFile(this.StorageUrl + "bootstrapper/files/krnl.dll", this.KrnlDir + "\\krnl.dll");
				this.WriteConfig(0, this.DllVersion);
			}
			this.DisplayText("Downloading krnl.exe..");
			if (this.ReadConfig(1) != this.UiVersion || !File.Exists(this.KrnlDir + "\\KrnlUI.exe"))
			{
				this.wc.DownloadFile(this.StorageUrl + "bootstrapper/Krnl.7z", this.KrnlDir + "\\krnl.7z");
				this.WriteConfig(1, this.UiVersion);
			}
			this.DisplayText("Downloading community..");
			this.wc.DownloadFile(this.StorageUrl + "community/Community.7z", MainWindow.DataDir + "\\Community.7z");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022DE File Offset: 0x000004DE
		private string ReadConfig(int Line)
		{
			return File.ReadAllLines(MainWindow.DataDir + "\\krnl.config")[Line];
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022F8 File Offset: 0x000004F8
		private void WriteConfig(int Line, string Data)
		{
			string[] array = File.ReadAllLines(MainWindow.DataDir + "\\krnl.config");
			array[Line] = Data;
			File.WriteAllLines(MainWindow.DataDir + "\\krnl.config", array);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002334 File Offset: 0x00000534
		private string GetHash(string FileName)
		{
			string @string;
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = File.OpenRead(FileName))
				{
					@string = Encoding.Default.GetString(md.ComputeHash(fileStream));
				}
			}
			return @string;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002394 File Offset: 0x00000594
		private void DisplayText(string Message)
		{
			base.Dispatcher.Invoke(delegate()
			{
				this.Message.Content = Message;
			});
		}

		// Token: 0x04000001 RID: 1
		private string KrnlDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Krnl";

		// Token: 0x04000002 RID: 2
		public static string DataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Krnl\\Data";

		// Token: 0x04000003 RID: 3
		public static string CommunityDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Krnl\\Community";

		// Token: 0x04000004 RID: 4
		private string ConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Krnl\\Data\\krnl.config";

		// Token: 0x04000005 RID: 5
		private string DllVersion = "0";

		// Token: 0x04000006 RID: 6
		private string UiVersion = "0";

		// Token: 0x04000007 RID: 7
		private string SiteUrl = "https://krnl.ca/";

		// Token: 0x04000008 RID: 8
		private string StorageUrl = "https://k-storage.com/";

		// Token: 0x04000009 RID: 9
		private string DediUrl = "https://cdn.krnl.ca/";

		// Token: 0x0400000A RID: 10
		private WebClient wc = new WebClient
		{
			Proxy = null
		};
	}
}
