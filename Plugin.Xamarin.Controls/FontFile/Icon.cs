using Plugin.Xamarin.Controls.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Xamarin.Controls.FontFile
{
    public class Icon : IIcon
    {
        /// <summary>
        /// The character matching the key in the font, for example '\u4354'
        /// </summary>
        public char Character { get; private set; }

        /// <summary>
        /// The key of icon, for example 'fa-ok'
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Icon" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="character">The character.</param>
        public Icon(string key, char character)
        {
            Character = character;
            Key = key;
        }
    }
}
