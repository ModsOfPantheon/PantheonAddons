#if NET6_0
// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Reserved for use by a compiler for tracking metadata.
    /// This attribute should not be used by developers in source code.
    /// </summary>
    //[EditorBrowsable(EditorBrowsableState.Never)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Delegate | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Struct, Inherited=false)]
    public sealed class NullableContextAttribute : Attribute
    {
        /// <summary>Initializes the attribute.</summary>
        /// <param name="value">The flags value.</param>
        public NullableContextAttribute(byte value)
        {
        }

        /// <summary>Initializes the attribute.</summary>
        /// <param name="value">The flags value.</param>
        public NullableContextAttribute(byte[] value)
        {
        }
    }
}
#endif