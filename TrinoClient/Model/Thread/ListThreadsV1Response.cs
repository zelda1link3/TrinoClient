﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TrinoClient.Model.Server;

namespace TrinoClient.Model.Thread
{
    /// <summary>
    /// A response for a request to list the presto threads
    /// </summary>
    public class ListThreadsV1Response
    {
        #region Public Properties

        /// <summary>
        /// The raw JSON content returned from presto
        /// </summary>
        public string RawContent { get; }

        /// <summary>
        /// The deserialized json. If deserialization fails, this will be null.
        /// </summary>
        public IEnumerable<ThreadResource> Threads { get; set; }

        /// <summary>
        /// Indicates whether deserialization was successful.
        /// </summary>
        public bool DeserializationSucceeded { get; }

        /// <summary>
        /// If deserialization fails, the will contain the thrown exception. Otherwise, 
        /// this property is null.
        /// </summary>
        public Exception LastError { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new response from the JSON array string returned from presto.
        /// </summary>
        /// <param name="rawContent">The JSON array of threads</param>
        internal ListThreadsV1Response(string rawContent)
        {
            RawContent = rawContent;

            if (!string.IsNullOrEmpty(RawContent))
            {
                try
                {
                    Threads = JsonConvert.DeserializeObject<IEnumerable<ThreadResource>>(RawContent);
                    DeserializationSucceeded = true;
                    LastError = null;
                }
                catch (Exception e)
                {
                    DeserializationSucceeded = false;
                    LastError = e;
                    Threads = null;
                }
            }
        }

        #endregion
    }
}
