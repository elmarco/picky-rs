// <auto-generated/> by Diplomat

#pragma warning disable 0105
using System;
using System.Runtime.InteropServices;

using Devolutions.Picky.Diplomat;
#pragma warning restore 0105

namespace Devolutions.Picky.Raw;

#nullable enable

[StructLayout(LayoutKind.Sequential)]
public partial struct SpcLink
{
#if __IOS__
    private const string NativeLib = "libDevolutionsPicky.framework/libDevolutionsPicky";
#else
    private const string NativeLib = "DevolutionsPicky";
#endif

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SpcLink_get_type", ExactSpelling = true)]
    public static unsafe extern SpcLinkType GetType(SpcLink* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SpcLink_get_url", ExactSpelling = true)]
    public static unsafe extern VecU8* GetUrl(SpcLink* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SpcLink_get_moniker", ExactSpelling = true)]
    public static unsafe extern SpcSerializedObject* GetMoniker(SpcLink* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SpcLink_get_file", ExactSpelling = true)]
    public static unsafe extern SpcString* GetFile(SpcLink* self);

    [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SpcLink_destroy", ExactSpelling = true)]
    public static unsafe extern void Destroy(SpcLink* self);
}
