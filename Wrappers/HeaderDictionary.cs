﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;

namespace Microsoft.AspNetCore.Server.EmbedIO
{
    public class HeaderDictionary : IHeaderDictionary
    {
        public NameValueCollection Collection { get; }

        public HeaderDictionary(NameValueCollection collection)
        {
            Collection = collection;
        }

        public virtual StringValues this[string key]
        {
            get
            {
                return Collection.GetValues(key);
            }
            set
            {
                Collection[key] = value;
            }
        }

        public int Count => Collection.Count;
        public bool IsReadOnly => false;

        public ICollection<string> Keys => Collection.Keys.OfType<string>().ToList();
        public ICollection<StringValues> Values => Collection.Keys.OfType<string>().Select(k => Collection.GetValues(k)).Cast<StringValues>().ToList();

        public void Add(KeyValuePair<string, StringValues> item) => Add(item.Key, item.Value);
        public void Add(string key, StringValues value)
        {
            Collection.Add(key, value);
        }
        public bool Remove(KeyValuePair<string, StringValues> item) => Remove(item.Key);
        public bool Remove(string key)
        {
            if (!ContainsKey(key))
                return false;

            Collection.Remove(key);
            return true;
        }
        public void Clear()
        {
            Collection.Clear();
        }

        public bool Contains(KeyValuePair<string, StringValues> item) => ContainsKey(item.Key);
        public bool ContainsKey(string key)
        {
            return Collection.Keys.OfType<string>().Contains(key);
        }

        public void CopyTo(KeyValuePair<string, StringValues>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out StringValues value)
        {
            if (ContainsKey(key))
            {
                value = Collection.GetValues(key);
                return true;
            }
            else
            {
                value = default(StringValues);
                return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator()
        {
            foreach (string key in Collection.Keys)
                yield return new KeyValuePair<string, StringValues>(key, Collection.GetValues(key));
        }
    }

    public class ResponseHeaderDictionary : HeaderDictionary
    {
        public HttpListenerResponse Respnse { get; }

        public override StringValues this[string key]
        {
            get
            {
                return base[key];
            }
            set
            {
                if (key == "Content-Length")
                    return;

                base[key] = value;
            }
        }

        public ResponseHeaderDictionary(HttpListenerResponse response) : base(response.Headers)
        {
            Respnse = response;
        }
    }
}
