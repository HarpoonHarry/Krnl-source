using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Krnl.Properties
{
	// Token: 0x02000005 RID: 5
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000012 RID: 18 RVA: 0x0000268D File Offset: 0x0000088D
		internal Resources()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002695 File Offset: 0x00000895
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Krnl.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000026C1 File Offset: 0x000008C1
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000026C8 File Offset: 0x000008C8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000026D0 File Offset: 0x000008D0
		internal static byte[] _7z_NET
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("_7z_NET", Resources.resourceCulture);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000026EB File Offset: 0x000008EB
		internal static byte[] _7za
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("_7za", Resources.resourceCulture);
			}
		}

		// Token: 0x0400000D RID: 13
		private static ResourceManager resourceMan;

		// Token: 0x0400000E RID: 14
		private static CultureInfo resourceCulture;
	}
}
