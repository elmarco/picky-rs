// <auto-generated/> by Diplomat

#pragma warning disable 0105
using System;
using System.Runtime.InteropServices;

using Devolutions.Picky.Diplomat;
#pragma warning restore 0105

namespace Devolutions.Picky;

#nullable enable

public partial class IssuerAndSerialNumber: IDisposable
{
    private unsafe Raw.IssuerAndSerialNumber* _inner;

    public string Issuer
    {
        get
        {
            return GetIssuer();
        }
    }

    /// <summary>
    /// Creates a managed <c>IssuerAndSerialNumber</c> from a raw handle.
    /// </summary>
    /// <remarks>
    /// Safety: you should not build two managed objects using the same raw handle (may causes use-after-free and double-free).
    /// <br/>
    /// This constructor assumes the raw struct is allocated on Rust side.
    /// If implemented, the custom Drop implementation on Rust side WILL run on destruction.
    /// </remarks>
    public unsafe IssuerAndSerialNumber(Raw.IssuerAndSerialNumber* handle)
    {
        _inner = handle;
    }

    /// <exception cref="PickyException"></exception>
    public void GetIssuer(DiplomatWriteable writable)
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("IssuerAndSerialNumber");
            }
            IntPtr resultPtr = Raw.IssuerAndSerialNumber.GetIssuer(_inner, &writable);
            Raw.X509SingerInfoFfiResultVoidBoxPickyError result = Marshal.PtrToStructure<Raw.X509SingerInfoFfiResultVoidBoxPickyError>(resultPtr);
            Raw.X509SingerInfoFfiResultVoidBoxPickyError.Destroy(resultPtr);
            if (!result.isOk)
            {
                throw new PickyException(new PickyError(result.Err));
            }
        }
    }

    /// <exception cref="PickyException"></exception>
    public string GetIssuer()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("IssuerAndSerialNumber");
            }
            DiplomatWriteable writeable = new DiplomatWriteable();
            IntPtr resultPtr = Raw.IssuerAndSerialNumber.GetIssuer(_inner, &writeable);
            Raw.X509SingerInfoFfiResultVoidBoxPickyError result = Marshal.PtrToStructure<Raw.X509SingerInfoFfiResultVoidBoxPickyError>(resultPtr);
            Raw.X509SingerInfoFfiResultVoidBoxPickyError.Destroy(resultPtr);
            if (!result.isOk)
            {
                throw new PickyException(new PickyError(result.Err));
            }
            string retVal = writeable.ToUnicode();
            writeable.Dispose();
            return retVal;
        }
    }

    /// <summary>
    /// Returns the underlying raw handle.
    /// </summary>
    public unsafe Raw.IssuerAndSerialNumber* AsFFI()
    {
        return _inner;
    }

    /// <summary>
    /// Destroys the underlying object immediately.
    /// </summary>
    public void Dispose()
    {
        unsafe
        {
            if (_inner == null)
            {
                return;
            }

            Raw.IssuerAndSerialNumber.Destroy(_inner);
            _inner = null;

            GC.SuppressFinalize(this);
        }
    }

    ~IssuerAndSerialNumber()
    {
        Dispose();
    }
}