using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;

namespace ns0
{
	// Token: 0x02000007 RID: 7
	internal class Class1
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00003510 File Offset: 0x00001710
		private static string smethod_0(Assembly assembly_0)
		{
			string text = assembly_0.FullName;
			int num = text.IndexOf(',');
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			return text;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000353C File Offset: 0x0000173C
		private static byte[] smethod_1(Assembly assembly_0)
		{
			try
			{
				string fullName = assembly_0.FullName;
				int num = fullName.IndexOf("PublicKeyToken=");
				if (num < 0)
				{
					num = fullName.IndexOf("publickeytoken=");
				}
				byte[] result;
				if (num < 0)
				{
					result = null;
					return result;
				}
				num += 15;
				if (fullName[num] != 'n')
				{
					if (fullName[num] != 'N')
					{
						string s = fullName.Substring(num, 16);
						long value = long.Parse(s, NumberStyles.HexNumber);
						byte[] bytes = BitConverter.GetBytes(value);
						Array.Reverse(bytes);
						result = bytes;
						return result;
					}
				}
				result = null;
				return result;
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000035DC File Offset: 0x000017DC
		internal static byte[] smethod_2(Stream stream_0)
		{
			byte[] result;
			lock (Class1.object_0)
			{
				result = Class1.smethod_4(97L, stream_0);
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003620 File Offset: 0x00001820
		internal static byte[] smethod_3(long long_0, Stream stream_0)
		{
			byte[] result;
			try
			{
				result = Class1.smethod_2(stream_0);
			}
			catch (HostProtectionException)
			{
				result = Class1.smethod_4(97L, stream_0);
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000365C File Offset: 0x0000185C
		internal static byte[] smethod_4(long long_0, Stream stream_0)
		{
			Stream stream = stream_0;
			MemoryStream memoryStream = null;
			for (int i = 1; i < 4; i++)
			{
				stream_0.ReadByte();
			}
			ushort num = (ushort)stream_0.ReadByte();
			num = ~num;
			if ((num & 2) != 0)
			{
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				byte[] array = new byte[8];
				stream_0.Read(array, 0, 8);
				dESCryptoServiceProvider.IV = array;
				byte[] array2 = new byte[8];
				stream_0.Read(array2, 0, 8);
				bool flag = true;
				byte[] array3 = array2;
				for (int j = 0; j < array3.Length; j++)
				{
					if (array3[j] != 0)
					{
						flag = false;
						IL_8B:
						if (flag)
						{
							array2 = Class1.smethod_1(Assembly.GetExecutingAssembly());
						}
						dESCryptoServiceProvider.Key = array2;
						if (Class1.memoryStream_0 == null)
						{
							if (Class1.int_0 == 2147483647)
							{
								Class1.memoryStream_0.Capacity = (int)stream_0.Length;
							}
							else
							{
								Class1.memoryStream_0.Capacity = Class1.int_0;
							}
						}
						Class1.memoryStream_0.Position = 0L;
						ICryptoTransform cryptoTransform = dESCryptoServiceProvider.CreateDecryptor();
						int inputBlockSize = cryptoTransform.InputBlockSize;
						int arg_105_0 = cryptoTransform.OutputBlockSize;
						byte[] array4 = new byte[cryptoTransform.OutputBlockSize];
						byte[] array5 = new byte[cryptoTransform.InputBlockSize];
						int num2 = (int)stream_0.Position;
						while ((long)(num2 + inputBlockSize) < stream_0.Length)
						{
							stream_0.Read(array5, 0, inputBlockSize);
							int count = cryptoTransform.TransformBlock(array5, 0, inputBlockSize, array4, 0);
							Class1.memoryStream_0.Write(array4, 0, count);
							num2 += inputBlockSize;
						}
						stream_0.Read(array5, 0, (int)(stream_0.Length - (long)num2));
						byte[] array6 = cryptoTransform.TransformFinalBlock(array5, 0, (int)(stream_0.Length - (long)num2));
						Class1.memoryStream_0.Write(array6, 0, array6.Length);
						stream = Class1.memoryStream_0;
						stream.Position = 0L;
						memoryStream = Class1.memoryStream_0;
						goto IL_1C6;
					}
				}
				goto IL_8B;
			}
			IL_1C6:
			if ((num & 8) != 0)
			{
				if (Class1.memoryStream_1 == null)
				{
					if (Class1.int_1 == -2147483648)
					{
						Class1.memoryStream_1.Capacity = (int)stream.Length * 2;
					}
					else
					{
						Class1.memoryStream_1.Capacity = Class1.int_1;
					}
				}
				Class1.memoryStream_1.Position = 0L;
				DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress);
				int num3 = 1000;
				byte[] buffer = new byte[1000];
				int num4;
				do
				{
					num4 = deflateStream.Read(buffer, 0, num3);
					if (num4 > 0)
					{
						Class1.memoryStream_1.Write(buffer, 0, num4);
					}
				}
				while (num4 >= num3);
				memoryStream = Class1.memoryStream_1;
			}
			if (memoryStream != null)
			{
				return memoryStream.ToArray();
			}
			byte[] array7 = new byte[stream_0.Length - stream_0.Position];
			stream_0.Read(array7, 0, array7.Length);
			return array7;
		}

		// Token: 0x04000019 RID: 25
		private static readonly object object_0 = new object();

		// Token: 0x0400001A RID: 26
		private static readonly int int_0 = 2147483647;

		// Token: 0x0400001B RID: 27
		private static readonly int int_1 = -2147483648;

		// Token: 0x0400001C RID: 28
		private static readonly MemoryStream memoryStream_0 = new MemoryStream(0);

		// Token: 0x0400001D RID: 29
		private static readonly MemoryStream memoryStream_1 = new MemoryStream(0);

		// Token: 0x0400001E RID: 30
		private static readonly byte byte_0;
	}
}
