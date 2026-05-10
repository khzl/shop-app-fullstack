using System;
using System.Collections.Generic;
using System.Text;

namespace Wpf.UI.ClientServices
{
    // Token Storage (Local)
    public static class TokenStore
    {
        // properties
        public static string? AccessToken { get; set; }
        public static string? RefreshToken { get; set; }

        // Procedure Clear
        public static void Clear()
        {
            AccessToken = null;
            RefreshToken = null;
        }

    }
}
