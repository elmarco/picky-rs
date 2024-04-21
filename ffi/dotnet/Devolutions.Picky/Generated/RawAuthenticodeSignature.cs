// <auto-generated/> by Diplomat

#pragma warning disable 0105
using System;
using System.Runtime.InteropServices;

using Devolutions.Picky.Diplomat;
#pragma warning restore 0105

namespace Devolutions.Picky.Raw;

#nullable enable

[StructLayout(LayoutKind.Sequential)]
public partial struct AuthenticodeSignature
{
#if __IOS__
    private const string NativeLib = "libDevolutionsPicky.framework/libDevolutionsPicky";
#else
    private const string NativeLib = "DevolutionsPicky";
#endif

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "AuthenticodeSignature_new", ExactSpelling = true)]
    public static unsafe extern IntPtr New(Pkcs7* pkcs7, VecU8* fileHash, ShaVariant hashAlgorithm, PrivateKey* privateKey, RsString* programName);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "AuthenticodeSignature_timestamp", ExactSpelling = true)]
    public static unsafe extern IntPtr Timestamp(AuthenticodeSignature* self, AuthenticodeTimestamper* timestamper, HashAlgorithm hashAlgo);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "AuthenticodeSignature_from_der", ExactSpelling = true)]
    public static unsafe extern IntPtr FromDer(VecU8* der);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "AuthenticodeSignature_from_pem", ExactSpelling = true)]
    public static unsafe extern IntPtr FromPem(Pem* pem);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "AuthenticodeSignature_from_pem_str", ExactSpelling = true)]
    public static unsafe extern IntPtr FromPemStr(byte* pem, nuint pemSz);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "AuthenticodeSignature_to_der", ExactSpelling = true)]
    public static unsafe extern IntPtr ToDer(AuthenticodeSignature* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "AuthenticodeSignature_to_pem", ExactSpelling = true)]
    public static unsafe extern IntPtr ToPem(AuthenticodeSignature* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "AuthenticodeSignature_signing_certificate", ExactSpelling = true)]
    public static unsafe extern IntPtr SigningCertificate(AuthenticodeSignature* self, CertIterator* cert);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "AuthenticodeSignature_authenticode_verifier", ExactSpelling = true)]
    public static unsafe extern AuthenticodeValidator* AuthenticodeVerifier(AuthenticodeSignature* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "AuthenticodeSignature_file_hash", ExactSpelling = true)]
    public static unsafe extern VecU8* FileHash(AuthenticodeSignature* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "AuthenticodeSignature_authenticate_attributes", ExactSpelling = true)]
    public static unsafe extern AttributeIterator* AuthenticateAttributes(AuthenticodeSignature* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "AuthenticodeSignature_unauthenticated_attributes", ExactSpelling = true)]
    public static unsafe extern UnsignedAttributeIterator* UnauthenticatedAttributes(AuthenticodeSignature* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "AuthenticodeSignature_destroy", ExactSpelling = true)]
    public static unsafe extern void Destroy(AuthenticodeSignature* self);
}