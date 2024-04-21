// <auto-generated/> by Diplomat

#pragma warning disable 0105
using System;
using System.Runtime.InteropServices;

using Devolutions.Picky.Diplomat;
#pragma warning restore 0105

namespace Devolutions.Picky.Raw;

#nullable enable

/// <summary>
/// Known key kinds
/// </summary>
public enum KeyKind
{
    /// <summary>
    /// RSA (Rivest–Shamir–Adleman)
    /// </summary>
    Rsa = 0,
    /// <summary>
    /// Elliptic-curve
    /// </summary>
    Ec = 1,
    /// <summary>
    /// Edwards-curve
    /// </summary>
    Ed = 2,
}