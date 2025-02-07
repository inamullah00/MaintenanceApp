using System.Text.RegularExpressions;

namespace Maintenance.Application.Helper
{
    public static class NormalizeNamesHelper
    {
        public static string NormalizeNames(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return string.Empty;

            return Regex.Replace(name.Trim(), @"\s+", " ");
        }
    }
}
