using System;

namespace SingleSignOn.Utils.ExtensionMethods
{
    public static class ByteArrayExtensionMethods
    {
        public static int ToInt(this byte[] byteArray)
        {
            // If the system architecture is little-endian (that is, little end first),
            // reverse the byte array.
            if (BitConverter.IsLittleEndian)
                Array.Reverse(byteArray);

            int i = BitConverter.ToInt32(byteArray, 0);
            return i;
        }
    }
}
