using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;

namespace BitDiffer.Common.Utility
{
	public static class GenericUtility
	{
		public static byte[] ReadStream(Assembly assembly, string name)
		{
			using (Stream stream = assembly.GetManifestResourceStream(name))
			{
				if (stream == null)
				{
					return null;
				}

				return ReadStream(stream);
			}
		}

		public static byte[] ReadStream(Stream stream)
		{
			int offset = 0;
			int remain = (int)stream.Length;
			byte[] content = new byte[stream.Length];

			while (remain > 0)
			{
				int read = stream.Read(content, offset, remain);
				remain -= read;
				offset += read;

				System.Diagnostics.Debug.Assert(remain >= 0);
			}

			return content;
		}

		public static bool CompareBytes(byte[] array1, byte[] array2)
		{
			if ((array1 == null) && (array2 == null))
			{
				return true;
			}

			if ((array1 == null) || (array2 == null))
			{
				return false;
			}

			if (array1.Length != array2.Length)
			{
				return false;
			}

			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}

			return true;
		}

		public static string GetILAsHashedText(MethodBase method)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				new MethodStreamer(method).WriteImplementationToStream(ms);

				return GetHashText(ms.ToArray());
			}
		}

		static char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

		public static string GetBytesAsText(byte[] bytes)
		{
			if ((bytes == null) || (bytes.Length == 0))
			{
				return null;
			}

			char[] chars = new char[bytes.Length * 2];

			for (int i = 0; i < bytes.Length; i++)
			{
				int b = bytes[i];
				chars[i * 2] = hexDigits[b >> 4];
				chars[i * 2 + 1] = hexDigits[b & 0xF];
			}
			return new string(chars);
		}

		public static string GetHashText(byte[] content)
		{
			SHA1Managed sha1 = new SHA1Managed();
			byte[] hashBytes = sha1.ComputeHash(content);
			return GetBytesAsText(hashBytes);
		}

		public static string GetHashText(string content)
		{
			return GetHashText(System.Text.Encoding.UTF8.GetBytes(content));
		}
	}
}
