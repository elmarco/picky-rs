// <auto-generated/> by Diplomat

#pragma warning disable 0105
using System;
using System.Runtime.InteropServices;

using Devolutions.Picky.Diplomat;
#pragma warning restore 0105

namespace Devolutions.Picky;

#nullable enable

public partial class AttributeValues: IDisposable
{
    private unsafe Raw.AttributeValues* _inner;

    public AttributeValueType Type
    {
        get
        {
            return GetType();
        }
    }

    /// <summary>
    /// Creates a managed <c>AttributeValues</c> from a raw handle.
    /// </summary>
    /// <remarks>
    /// Safety: you should not build two managed objects using the same raw handle (may causes use-after-free and double-free).
    /// <br/>
    /// This constructor assumes the raw struct is allocated on Rust side.
    /// If implemented, the custom Drop implementation on Rust side WILL run on destruction.
    /// </remarks>
    public unsafe AttributeValues(Raw.AttributeValues* handle)
    {
        _inner = handle;
    }

    /// <returns>
    /// A <c>AttributeValueType</c> allocated on C# side.
    /// </returns>
    public AttributeValueType GetType()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("AttributeValues");
            }
            Raw.AttributeValueType retVal = Raw.AttributeValues.GetType(_inner);
            return (AttributeValueType)retVal;
        }
    }

    /// <returns>
    /// A <c>VecU8</c> allocated on Rust side.
    /// </returns>
    public VecU8? ToCustom()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("AttributeValues");
            }
            Raw.VecU8* retVal = Raw.AttributeValues.ToCustom(_inner);
            if (retVal == null)
            {
                return null;
            }
            return new VecU8(retVal);
        }
    }

    /// <returns>
    /// A <c>ExtensionIterator</c> allocated on Rust side.
    /// </returns>
    public ExtensionIterator? ToExtensions()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("AttributeValues");
            }
            Raw.ExtensionIterator* retVal = Raw.AttributeValues.ToExtensions(_inner);
            if (retVal == null)
            {
                return null;
            }
            return new ExtensionIterator(retVal);
        }
    }

    /// <returns>
    /// A <c>StringIterator</c> allocated on Rust side.
    /// </returns>
    public StringIterator? ToContentType()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("AttributeValues");
            }
            Raw.StringIterator* retVal = Raw.AttributeValues.ToContentType(_inner);
            if (retVal == null)
            {
                return null;
            }
            return new StringIterator(retVal);
        }
    }

    /// <returns>
    /// A <c>StringNestedIterator</c> allocated on Rust side.
    /// </returns>
    public StringNestedIterator? ToSpcStatementType()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("AttributeValues");
            }
            Raw.StringNestedIterator* retVal = Raw.AttributeValues.ToSpcStatementType(_inner);
            if (retVal == null)
            {
                return null;
            }
            return new StringNestedIterator(retVal);
        }
    }

    /// <returns>
    /// A <c>StringIterator</c> allocated on Rust side.
    /// </returns>
    public StringIterator? ToMessageDigest()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("AttributeValues");
            }
            Raw.StringIterator* retVal = Raw.AttributeValues.ToMessageDigest(_inner);
            if (retVal == null)
            {
                return null;
            }
            return new StringIterator(retVal);
        }
    }

    /// <returns>
    /// A <c>UTCTimeIterator</c> allocated on Rust side.
    /// </returns>
    public UTCTimeIterator? ToSigningTime()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("AttributeValues");
            }
            Raw.UTCTimeIterator* retVal = Raw.AttributeValues.ToSigningTime(_inner);
            if (retVal == null)
            {
                return null;
            }
            return new UTCTimeIterator(retVal);
        }
    }

    /// <returns>
    /// A <c>SpcSpOpusInfoIterator</c> allocated on Rust side.
    /// </returns>
    public SpcSpOpusInfoIterator? ToSpcSpOpusInfo()
    {
        unsafe
        {
            if (_inner == null)
            {
                throw new ObjectDisposedException("AttributeValues");
            }
            Raw.SpcSpOpusInfoIterator* retVal = Raw.AttributeValues.ToSpcSpOpusInfo(_inner);
            if (retVal == null)
            {
                return null;
            }
            return new SpcSpOpusInfoIterator(retVal);
        }
    }

    /// <summary>
    /// Returns the underlying raw handle.
    /// </summary>
    public unsafe Raw.AttributeValues* AsFFI()
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

            Raw.AttributeValues.Destroy(_inner);
            _inner = null;

            GC.SuppressFinalize(this);
        }
    }

    ~AttributeValues()
    {
        Dispose();
    }
}
