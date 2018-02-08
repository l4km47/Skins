using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace GorkerPrivate.Properties
{
	// Token: 0x02000006 RID: 6
	[DebuggerNonUserCode, CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	public class Resources
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000020E6 File Offset: 0x000002E6
		internal Resources()
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00003468 File Offset: 0x00001668
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceManager_0, null))
				{
					ResourceManager resourceManager = new ResourceManager("GorkerPrivate.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceManager_0 = resourceManager;
				}
				return Resources.resourceManager_0;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000034AC File Offset: 0x000016AC
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020EE File Offset: 0x000002EE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return Resources.cultureInfo_0;
			}
			set
			{
				Resources.cultureInfo_0 = value;
			}
		}

		// Token: 0x04000017 RID: 23
		private static ResourceManager resourceManager_0;

		// Token: 0x04000018 RID: 24
		private static CultureInfo cultureInfo_0;
	}
}
