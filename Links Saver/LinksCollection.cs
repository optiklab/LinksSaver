using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AYarkov.LinksSaver
{
    #region LinksCollection
    ///<summary>
    /// Collection of LinkRecord objects
    ///</summary>
    [XmlRoot("LinksList")]
    public class LinksCollection
    {
        #region Public Member Variables
        [XmlElement("linkitem")]
        public List<LinkRecord> Collection;
        #endregion

        #region Constructor
        public LinksCollection()
        {
            Collection = new List<LinkRecord>();
        }
        #endregion

        #region Public methods
        //Adds new link to the collection
        //Parent = theme name
        //Name = link name (shows in tree View)
        //URL = url
        //Thrown exception in case Link with the same name already exists or OutOfMemoryException
        public void AddNew(String Parent, String Name, String URL)  //exception
        {
            if (IsUnique(Name) == true)
            {
                try
                {
                    LinkRecord item = new LinkRecord(Parent, Name, URL);
                    Collection.Add(item);
                }
                catch (OutOfMemoryException)
                {
                    throw new ItemException(AYarkov.LinksSaver.Properties.Resources.MemoryError);
                }
            }
            else
            {
                throw new ItemException(AYarkov.LinksSaver.Properties.Resources.SameName);
            }
        }

        //Deletes link with Name from the collection and return TRUE
        //If such node was not found return FALSE
        public bool Delete(String Name)
        {
            try
            {
                foreach (LinkRecord lnk in Collection)
                {
                    if (lnk._name == Name)
                    {
                        Collection.Remove(lnk);
                        return true;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
            return true;
        }

        //Delete all link with theme ThemeName and return TRUE
        //In case some error return FALSE
        public bool DeleteByTheme(String ThemeName)
        {
            try
            {
                ArrayList matches = new ArrayList();
                foreach(LinkRecord lnk in Collection)
                {
                    if(lnk._parent == ThemeName)
                        matches.Add(lnk._name);
                }
                for (int i = 0; i < matches.Count; i++)
                {
                    Delete(matches[i].ToString());
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
            return true;
        }

        //Rename node with Index in the collection to NewName
        public void Rename(int Index, String NewName)
        {
            if (IsUnique(NewName) == true)
            {
                Collection[Index]._name = NewName;
            }
            else
            {
                throw new ItemException(AYarkov.LinksSaver.Properties.Resources.SameName);
            }
        }

        //Redirect group of links from ThemeName to NewTheme
        public void ChangeThemeInGroup(String ThemeName, String NewTheme)
        {
            foreach (LinkRecord lnk in Collection)
            {
                if (lnk._parent == ThemeName)
                    lnk._parent = NewTheme;
            }
        }

        //Returns URL of link with Name
        //If link not found: return Empty String
        public String GetURL(String Name)
        {
            foreach (LinkRecord lnk in Collection)
            {
                if (lnk._name == Name)
                    return lnk._url;
            }
            return String.Empty;
        }

        //Returns ThemeName of link with Name
        //If link not found: return Empty String
        public String GetParentTheme(String Name)
        {
            foreach (LinkRecord lnk in Collection)
            {
                if (lnk._name == Name)
                    return lnk._parent;
            }
            return String.Empty;
        }

        //Return count of items in the collection
        public int GetCount()
        {
            return Collection.Count;
        }


        //Method search for Name node in the collection and return it
        //In case nothing found return -1
        public int GetID(String Name)
        {
            for (int i = 0; i < Collection.Count; i++)
            {
                if (Collection[i]._name == Name)
                    return i;
            }
            return -1;
        }


        //Check that entire link is unique for current collection
        public bool IsUnique(String NameToCheck)
        {
            foreach (LinkRecord lnk in Collection)
            {
                if (lnk._name == NameToCheck)
                    return false;
            }
            return true;
        }
        #endregion
    }
    #endregion
}
