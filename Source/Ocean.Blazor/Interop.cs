namespace Oceanware.Ocean.Blazor {

    using System;
    using System.Threading.Tasks;
    using Microsoft.JSInterop;

    /// <summary>
    /// Class Interop.
    /// </summary>
    public static class Interop {

        /// <summary>
        /// Sets focus to the specified elementId
        /// </summary>
        /// <param name="jsRuntime">The js runtime.</param>
        /// <param name="elementId">The element identifier.</param>
        /// <returns>ValueTask&lt;Object&gt;.</returns>
        public static ValueTask<Object> Focus(IJSRuntime jsRuntime, String elementId) {
            return jsRuntime.InvokeAsync<Object>("oceanwareOceanBlazor.setFocus", elementId);
        }

        /// <summary>
        /// Scrolls the automatic complete results listing to the specified element.
        /// </summary>
        /// <param name="jsRuntime">The js runtime.</param>
        /// <param name="elementId">The element identifier.</param>
        /// <returns>ValueTask&lt;Object&gt;.</returns>
        internal static ValueTask<Object> ScrollAutoCompleteIfNeeded(IJSRuntime jsRuntime, String elementId) {
            return jsRuntime.InvokeAsync<Object>("oceanwareOceanBlazor.scrollAutoCompleteIfNeeded", elementId);
        }
    }
}
