﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

internal static partial class Interop
{
    internal static partial class Crypto
    {
        [DllImport(Libraries.CryptoNative)]
        internal static extern SafeX509ExtensionHandle X509ExtensionCreateByObj(
            SafeAsn1ObjectHandle oid,
            [MarshalAs(UnmanagedType.Bool)] bool isCritical,
            SafeAsn1OctetStringHandle data);

        [DllImport(Libraries.CryptoNative)]
        internal static extern int X509ExtensionDestroy(IntPtr x);

        [DllImport(Libraries.CryptoNative)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool X509V3ExtPrint(SafeBioHandle buf, SafeX509ExtensionHandle ext);

        [DllImport(Libraries.CryptoNative)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DecodeX509BasicConstraints2Extension(
            byte[] encoded,
            int encodedLength,
            [MarshalAs(UnmanagedType.Bool)] out bool certificateAuthority,
            [MarshalAs(UnmanagedType.Bool)] out bool hasPathLengthConstraint,
            out int pathLengthConstraint);

        [DllImport(Libraries.CryptoNative)]
        internal static extern SafeEkuExtensionHandle DecodeExtendedKeyUsage(byte[] buf, int len);

        [DllImport(Libraries.CryptoNative)]
        internal static extern void ExtendedKeyUsageDestory(IntPtr a);
    }
}
