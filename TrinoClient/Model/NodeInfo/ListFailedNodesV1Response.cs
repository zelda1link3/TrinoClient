﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TrinoClient.Model.FailureDetector;

namespace TrinoClient.Model.NodeInfo
{
    /// <summary>
    /// A response from a request to list the nodes in the cluster
    /// </summary>
    public class ListFailedNodesV1Response
    {
        #region Public Properties

        /// <summary>
        /// The raw JSON content returned from presto
        /// </summary>
        public string RawContent { get; }

        /// <summary>
        /// The deserialized json. If deserialization fails, this will be null.
        /// </summary>
        public IEnumerable<HeartbeatFailureDetectorStats> FailedNodes { get; set; }

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
        /// <param name="rawContent">The JSON array of nodes</param>
        internal ListFailedNodesV1Response(string rawContent)
        {
            RawContent = rawContent;

            if (!string.IsNullOrEmpty(RawContent))
            {
                try
                {
                    FailedNodes = JsonConvert.DeserializeObject<IEnumerable<HeartbeatFailureDetectorStats>>(RawContent);
                    DeserializationSucceeded = true;
                    LastError = null;
                }
                catch (Exception e)
                {
                    DeserializationSucceeded = false;
                    LastError = e;
                    FailedNodes = null;
                }
            }
        }

        #endregion
    }
}
