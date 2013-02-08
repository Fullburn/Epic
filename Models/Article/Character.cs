using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpicProto
{
    public class Character : Article
    {
        /// <summary>
        /// Constructor used for deserialization.
        /// </summary>
        public Character() : base()
        {
            //StateManager.Current.AllCharacters.Add(this);
        }
    }
}
