using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AYarkov.LinksSaver
{
    #region Themes
    ///<summary>
    /// It is the data about theme
    ///</summary>
    public class Themes
    {
        #region Private Member Variables
        [XmlAttribute("ThemeName")]
        public String _name;
        #endregion

        #region Constructor
        public Themes(String Name)
        {
            _name = Name;
        }

        public Themes() :
            this("Default Theme")
        { }
        #endregion
    }
    #endregion
}
