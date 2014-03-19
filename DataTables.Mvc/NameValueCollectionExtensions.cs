#region Copyright
/* The MIT License (MIT)

Copyright (c) 2014 Anderson Luiz Mendes Matos

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion Copyright
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTables.Mvc
{
    /// <summary>
    /// Provides extension methods for use with NameValueCollections.
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// Gets a typed item from the collection using the provided key.
        /// If there's no corresponding item on the collection, returns default(T).
        /// </summary>
        /// <typeparam name="T">The type to cast the collection item.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key to access the item inside the collection.</param>
        /// <returns>The typed item.</returns>
        public static T G<T>(this NameValueCollection collection, string key) { return G<T>(collection, key, default(T)); }
        /// <summary>
        /// Gets a typed item from the collection using the provided key.
        /// If there's no corresponding item on the collection, returns the provided defaultValue.
        /// </summary>
        /// <typeparam name="T">The type to cast the collection item.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key to access the item inside the collection.</param>
        /// <param name="defaultValue">The default value to return if there's no corresponding item on the collection.</param>
        /// <returns>The typed item.</returns>
        public static T G<T>(this NameValueCollection collection, string key, object defaultValue)
        {
            if (collection == null) throw new ArgumentNullException("collection", "The provided collection cannot be null.");
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("The provided key cannot be null or empty.", "key");

            var collectionItem = collection[key];
            if (collectionItem == null) return (T)defaultValue;
            return (T)Convert.ChangeType(collectionItem, typeof(T));
        }
        /// <summary>
        /// Sets or updates a value inside the provided collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key to access the item inside the collection.</param>
        /// <param name="value">The value to be set or updated.</param>
        public static void S(this NameValueCollection collection, string key, object value)
        {
            if (collection == null) throw new ArgumentNullException("collection", "The provided collection cannot be null.");
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("The provided key cannot be null or empty.", "key");
            if (value == null) throw new ArgumentNullException("value", "The provided value cannot be null.");

            if (collection.Keys.Cast<string>().Any(_k => _k.Equals(key)))
                collection[key] = value.ToString();
            else
                collection.Add(key, value.ToString());
        }
    }
}
