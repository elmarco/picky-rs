// <auto-generated/> by Diplomat

#pragma warning disable 0105
using System;
using System.Runtime.InteropServices;

using Devolutions.Picky.Diplomat;
#pragma warning restore 0105

namespace Devolutions.Picky;

#nullable enable

public partial class AesParameters: IDisposable
{
    private unsafe Raw.AesParameters* _inner;

    public AesParametersType Type
    {
        get
        {
            return GetType();
        }
    }

    /// <summary>
    /// Creates a managed <c>AesParameters</c> from a raw handle.
    /// </summary>
    /// <remarks>
    /// Safety: you should not build two managed objects using the same raw handle (may causes use-after-free and double-free).
    /// <br/>
    /// This constructor assumes the raw struct is allocated on Rust side.
    /// If implemented, the custom Drop implementation on Rust side WILL run on destruction.
    /// </remarks>
    public unsafe AesParameters(Raw.AesParameters* handle)
    {
        _inner = handle;
    }

    /// <returns>
    /// A <c>AesParametersType</c> allocated on C# side.
    /// </returns>
    public AesParametersType GetType()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("AesParameters");
            }
            Raw.AesParametersType retVal = Raw.AesParameters.GetType(_inner);
            return (AesParametersType)retVal;
        }
    }

    /// <returns>
    /// A <c>VecU8</c> allocated on Rust side.
    /// </returns>
    public VecU8? ToInitializationVector()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("AesParameters");
            }
            Raw.VecU8* retVal = Raw.AesParameters.ToInitializationVector(_inner);
            if (retVal == null)
            {
                return null;
            }
            return new VecU8(retVal);
        }
    }

    /// <returns>
    /// A <c>AesAuthEncParams</c> allocated on Rust side.
    /// </returns>
    public AesAuthEncParams? ToAuthenticatedEncryptionParameters()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("AesParameters");
            }
            Raw.AesAuthEncParams* retVal = Raw.AesParameters.ToAuthenticatedEncryptionParameters(_inner);
            if (retVal == null)
            {
                return null;
            }
            return new AesAuthEncParams(retVal);
        }
    }

    /// <summary>
    /// Returns the underlying raw handle.
    /// </summary>
    public unsafe Raw.AesParameters* AsFFI()
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

            Raw.AesParameters.Destroy(_inner);
            _inner = null;

            GC.SuppressFinalize(this);
        }
    }

    ~AesParameters()
    {
        Dispose();
    }
}
