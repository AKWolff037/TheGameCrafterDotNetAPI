using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGCDotNetAPI
{
    /// <summary>
    /// This interface denotes that something is serializable in the context of an ITGCObject.
    /// This interface contains a result dictionary, raw result string, Get/Set Property methods, and a Parse method
    /// </summary>
    public interface ISerializable
    {
        /// <summary>
        /// The result dictionary.  Keys are strings and Values are objects
        /// </summary>
        Dictionary<string, object> result { get; }
        /// <summary>
        /// The raw JSON string result that is usually parsed into the dictionary
        /// </summary>
        string rawresult { get; set; }
        /// <summary>
        /// Returns the object value of the property at the given key
        /// </summary>
        /// <param name="key">The key in the result dictionary</param>
        /// <returns>The object value of the result[key]</returns>
        object GetProperty(string key);
        /// <summary>
        /// Sets a property's value in the dictionary
        /// </summary>
        /// <param name="key">The key in the result dictionary</param>
        /// <param name="value">The object to set the value to</param>
        void SetProperty(string key, object value);
        /// <summary>
        /// Parses the rawresult string to create the result dictionary
        /// </summary>
        void Parse();
    }
}
