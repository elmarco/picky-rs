// <auto-generated/> by Diplomat

#pragma warning disable 0105
using System;
using System.Runtime.InteropServices;

using Devolutions.Picky.Diplomat;
#pragma warning restore 0105

namespace Devolutions.Picky;

#nullable enable

public partial class EdiPartyName: IDisposable
{
    private unsafe Raw.EdiPartyName* _inner;

    public DirectoryString? NameAssigner
    {
        get
        {
            return GetNameAssigner();
        }
    }

    public DirectoryString PartyName
    {
        get
        {
            return GetPartyName();
        }
    }

    /// <summary>
    /// Creates a managed <c>EdiPartyName</c> from a raw handle.
    /// </summary>
    /// <remarks>
    /// Safety: you should not build two managed objects using the same raw handle (may causes use-after-free and double-free).
    /// <br/>
    /// This constructor assumes the raw struct is allocated on Rust side.
    /// If implemented, the custom Drop implementation on Rust side WILL run on destruction.
    /// </remarks>
    public unsafe EdiPartyName(Raw.EdiPartyName* handle)
    {
        _inner = handle;
    }

    /// <returns>
    /// A <c>DirectoryString</c> allocated on Rust side.
    /// </returns>
    public DirectoryString? GetNameAssigner()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("EdiPartyName");
            }
            Raw.DirectoryString* retVal = Raw.EdiPartyName.GetNameAssigner(_inner);
            if (retVal == null)
            {
                return null;
            }
            return new DirectoryString(retVal);
        }
    }

    /// <returns>
    /// A <c>DirectoryString</c> allocated on Rust side.
    /// </returns>
    public DirectoryString GetPartyName()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("EdiPartyName");
            }
            Raw.DirectoryString* retVal = Raw.EdiPartyName.GetPartyName(_inner);
            return new DirectoryString(retVal);
        }
    }

    /// <summary>
    /// Returns the underlying raw handle.
    /// </summary>
    public unsafe Raw.EdiPartyName* AsFFI()
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

            Raw.EdiPartyName.Destroy(_inner);
            _inner = null;

            GC.SuppressFinalize(this);
        }
    }

    ~EdiPartyName()
    {
        Dispose();
    }
}
