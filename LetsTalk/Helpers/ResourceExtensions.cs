﻿using Windows.ApplicationModel.Resources;

namespace LetsTalk.Helpers
{
    internal static class ResourceExtensions
    {
        private static ResourceLoader resLoader = new ResourceLoader();

        public static string GetLocalized(this string resourceKey)
        {
            return resLoader.GetString(resourceKey);
        }
    }
}
