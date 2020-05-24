using System.Linq;

namespace Siren.MobileAppService.Extensions
{
    public static class StringExtensions
    {
        public static int GetHashCodeSimple(this string val)
        {
            return val.Select(a => (int)a).Sum();
        }
    }
}
