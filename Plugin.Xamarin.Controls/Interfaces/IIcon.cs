using System;

namespace Plugin.Xamarin.Controls.Interfaces
{
    public interface IIcon
    {
        /// <summary>
        /// The key of icon, for example 'fa-ok'
        /// </summary>
        /// <returns></returns>
        string Key { get; }

        /// <summary>
        /// The character matching the key in the font, for example '\u4354'
        /// </summary>
        /// <returns></returns>
        char Character { get; }

    }
}
