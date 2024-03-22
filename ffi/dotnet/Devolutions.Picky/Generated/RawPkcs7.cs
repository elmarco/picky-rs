// <auto-generated/> by Diplomat

#pragma warning disable 0105
using System;
using System.Runtime.InteropServices;

using Devolutions.Picky.Diplomat;
#pragma warning restore 0105

namespace Devolutions.Picky.Raw;

#nullable enable

[StructLayout(LayoutKind.Sequential)]
public partial struct Pkcs7
{
#if __IOS__
    private const string NativeLib = "libDevolutionsPicky.framework/libDevolutionsPicky";
#else
    private const string NativeLib = "DevolutionsPicky";
#endif

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Pkcs7_from_der", ExactSpelling = true)]
    public static unsafe extern IntPtr FromDer(byte* data, nuint dataSz);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Pkcs7_from_pem", ExactSpelling = true)]
    public static unsafe extern IntPtr FromPem(Pem* pem);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Pkcs7_to_der", ExactSpelling = true)]
    public static unsafe extern IntPtr ToDer(Pkcs7* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Pkcs7_to_pem", ExactSpelling = true)]
    public static unsafe extern IntPtr ToPem(Pkcs7* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Pkcs7_digest_algorithms", ExactSpelling = true)]
    public static unsafe extern AlgorithmIdentifierIterator* DigestAlgorithms(Pkcs7* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Pkcs7_signer_infos", ExactSpelling = true)]
    public static unsafe extern SignerInfoIterator* SignerInfos(Pkcs7* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Pkcs7_encapsulated_content_info", ExactSpelling = true)]
    public static unsafe extern EncapsulatedContentInfo* EncapsulatedContentInfo(Pkcs7* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Pkcs7_decode_certificates", ExactSpelling = true)]
    public static unsafe extern CertIterator* DecodeCertificates(Pkcs7* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Pkcs7_destroy", ExactSpelling = true)]
    public static unsafe extern void Destroy(Pkcs7* self);
}
