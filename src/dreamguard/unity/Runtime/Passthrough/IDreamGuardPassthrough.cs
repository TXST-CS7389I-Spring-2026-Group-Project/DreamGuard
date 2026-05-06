namespace DreamGuard
{
    /// <summary>
    /// Common interface for all DreamGuard passthrough techniques.
    /// Lets DreamGuardMenu enable/disable techniques generically.
    /// </summary>
    public interface IDreamGuardPassthrough
    {

        /// <summary>Enable or disable the passthrough technique.</summary>
        void SetEnabled(bool value);
    }
}
