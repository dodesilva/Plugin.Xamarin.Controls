using Plugin.Xamarin.Controls.EnumFiles;
using Plugin.Xamarin.Controls.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Xamarin.Controls.FontFile;

namespace Plugin.Xamarin.Controls.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Adds the icon to the list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="key">The key.</param>
        /// <param name="character">The character.</param>
        public static void Add(this IList<IIcon> list, String key, Char character) => list.Add(new Icon(key, character));

        /// <summary>
        /// Retrieve an icon from a key.
        /// </summary>
        /// <param name="iconKey">The icon key.</param>
        /// /// <param name="font">The font of icon.</param>
        /// <returns>The icon, or null if no icon matches the key</returns>
        public static IIcon FindIconForKey(string iconKey, Fonts font)
        {
            if (string.IsNullOrWhiteSpace(iconKey))
            {
                return null;
            }
            switch (font)
            {
                case Fonts.FontAwesome:
                    return FontAwesomeCollection.Icons.FirstOrDefault(x => x.Key.Contains(iconKey));
                case Fonts.Material:
                    return MaterialCollection.Icons.FirstOrDefault(x => x.Key.Contains(iconKey));
                case Fonts.IconMoon:
                    return IconMoonCollection.Icons.FirstOrDefault(x => x.Key.Contains(iconKey));
                default:
                    return null;
            }

        }


        /// <summary>
        /// Retrieve a name of font icon.
        /// </summary>
        /// <param name="font">The font of icon.</param>
        /// <returns>The name font, or empty string if no font matches the Enum Fonts</returns>
        public static string FindNameForFont(Fonts font)
        {
            switch (font)
            {
                case Fonts.FontAwesome:
                    return "FontAwesome";
                case Fonts.Material:
                    return "MaterialIcons-Regular";
                case Fonts.IconMoon:
                    return "IconMoon";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Retrieve a name file of font icon.
        /// </summary>
        /// <param name="font">The font of icon.</param>
        /// <returns>The name file font, or empty string if no font matches the Enum Fonts</returns>
        public static string FindNameFileForFont(Fonts font)
        {
            switch (font)
            {
                case Fonts.FontAwesome:
                    return "fontawesome.ttf";
                case Fonts.Material:
                    return "materialicons.ttf";
                case Fonts.IconMoon:
                    return "icomoon.ttf";
                default:
                    return string.Empty;
            }
        }


        /// <summary>
        /// Retrieve a path of font icon.
        /// </summary>
        /// <param name="font">The font of icon.</param>
        /// <returns>The path file font, or empty string if no font matches the Enum Fonts</returns>
        public static string FindPathForFont(Fonts font)
        {
            switch (font)
            {
                case Fonts.FontAwesome:
                    return "/Assets/Fonts/fontawesome.ttf#FontAwesome";
                case Fonts.IconMoon:
                    return "/Assets/Fonts/icomoon.ttf#IconMoon";
                case Fonts.Material:
                    return "/Assets/Fonts/materialicons.ttf#Material Icons";
                default:
                    return string.Empty;
            }

        }


    }
}
