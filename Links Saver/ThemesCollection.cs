using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AYarkov.LinksSaver
{
    #region ThemesCollection
    ///<summary>
    /// Collection of Theme objects
    ///</summary>
    [XmlRoot("ThemesList")]
    public class ThemesCollection
    {
        #region Public Member Variables
        [XmlElement("themeitem")]
        public List<Themes> Collection;
        #endregion

        #region Constructor
        public ThemesCollection()
        {
            Collection = new List<Themes>();
        }
        #endregion

        #region Public methods
        //Adds new theme to the collection
        //Name = theme's name (shows in tree View)
        //Thrown exception in case Theme with the same name already exists or OutOfMemoryException
        public void AddNew(String Name)
        {
            if (IsUnique(Name) == true)
            {
                try
                {
                    Themes item = new Themes(Name);
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

        //Deletes theme with Name from the collection and return TRUE
        //If such node was not found return FALSE
        public bool DeleteTheme(string Name)
        {
            try
            {
                foreach (Themes th in Collection)
                {
                    if (th._name == Name)
                    {
                        Collection.Remove(th);
                        return true;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
            return false;
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

        //Check that entire theme is unique for current collection
        public bool IsUnique(String NameToCheck)
        {
            foreach (Themes th in Collection)
            {
                if (th._name == NameToCheck)
                    return false;
            }
            return true;
        }
        #endregion
    }
    #endregion
}
