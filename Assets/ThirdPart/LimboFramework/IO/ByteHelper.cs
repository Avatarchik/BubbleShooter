﻿using System;

namespace LimboFramework.IO
{
	public static class ByteHelper
    {
        private const int HandleBytesLength = 1024;
        private static readonly byte[] Key = { 9, 6, 4, 1, 7 };

        public static byte[] DeOrEncrypt(byte[] bytes)
        {
            int len = Math.Min(bytes.Length, HandleBytesLength);
            for (int i = 0; i < len; i++)
            {
                bytes[i] = (byte)(bytes[i] ^ Key[i % 5]);
            }
            return bytes;
        }
    }
}
