using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link10.AppServices
{
    internal static class Link10UriExtension
    {
        public static bool IsValidAppServiceUri(this Uri uri)
        {
            // needs at least host + pfn + app services name.
            return uri.Segments.Length >= 3;
        }

        public static string GetPackageFamilyName(this Uri uri)
        {
            return uri.Segments[1].Trim('/');
        }

        public static string GetAppServiceName(this Uri uri)
        {
            return uri.Segments[2].Trim('/');
        }

        public static string GetRoutedCall(this Uri uri)
        {
            return uri.Segments[3].Trim('/');
        }
    }
}
