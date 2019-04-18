using System;
using System.Security;
using System.Security.Cryptography;

namespace BlazorTests.Common.Technical.Security
{
    public class PasswordHelper
    {
        public static byte[] ConvertSecureStringToHash(SecureString value)
        {
            if (value == null)
            {
                return null;
            }

            return Hash(ConvertSecureStringToByteArray(value));
        }

        public static bool AreHashedPasswordEquals(byte[] hashedPassword1, byte[] hashedPassword2)
        {
            if (hashedPassword1 == null || hashedPassword2 == null)
            {
                return false;
            }

            if (hashedPassword1.Length != hashedPassword2.Length)
            {
                return false;
            }

            for (int i = 0; i < hashedPassword1.Length; i++)
            {
                if (hashedPassword1[i] != hashedPassword2[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static byte[] ConvertSecureStringToByteArray(SecureString value)
        {
            byte[] returnVal = new byte[value.Length];

            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(value);
                for (int i = 0; i < value.Length; i++)
                {
                    short unicodeChar = System.Runtime.InteropServices.Marshal.ReadInt16(valuePtr, i * 2);
                    returnVal[i] = Convert.ToByte(unicodeChar);
                }

                return returnVal;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        private static byte[] Hash(byte[] inputBytes)
        {
            SHA256Managed algorithm = new SHA256Managed();
            algorithm.ComputeHash(inputBytes);
            return algorithm.Hash;
        }
    }
}
