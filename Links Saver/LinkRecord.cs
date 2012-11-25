using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AYarkov.LinksSaver
{
    #region LinkRecord
    ///<summary>
    /// Link record - it is the data about link name and link URL
    ///</summary>
    public class LinkRecord : IComparable
    {
        #region Private Member Variables
        [XmlAttribute("ThemeNameID")]
        public String _parent;
        [XmlAttribute("URLName")]
        public String _name;
        [XmlAttribute("URLAddress")]
        public String _url;
        #endregion

        #region Constructor
        public LinkRecord(String Parent, String Name, String URL)
        {
            _parent = Parent;
            _name = Name;
            _url = URL;
        }

        public LinkRecord() :
            this("Default Theme", "Home", "http://www.optiklab.ru")
        { }
        #endregion

        #region Public Methods
        int IComparable.CompareTo(object Obj)
        {
            LinkRecord temp = (LinkRecord)Obj;
            if (this._name.CompareTo(temp._name) == 1 && this._url.CompareTo(temp._url) == 1)
                return 1;
            if (this._name.CompareTo(temp._name) == -1 || this._url.CompareTo(temp._url) == -1)
                return -1;
            else
                return 0;
        }
        #endregion
    }
    #endregion
}
