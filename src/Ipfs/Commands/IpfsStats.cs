﻿using Ipfs.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ipfs.Commands
{
    public class IpfsStats : IpfsCommand
    {
        internal IpfsStats(Uri commandUri, HttpClient httpClient, IJsonSerializer jsonSerializer) : base(commandUri, httpClient, jsonSerializer)
        {
        }

        /// <summary>
        /// Print ipfs bandwidth information
        /// </summary>
        /// <param name="peer">specify a peer to print bandwidth for</param>
        /// <param name="proto">specify a protocol to print bandwidth for</param>
        /// <param name="poll">print bandwidth at an interval</param>
        /// <param name="interval">time interval to wait between updating output</param>
        /// <param name="cancellationToken">Token allowing you to cancel the request</param>
        /// <returns></returns>
        public async Task<Json.IpfsStatsBw> Bw(string peer = null, string proto = null, bool poll = false, string interval = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var flags = new Dictionary<string, string>();

            if(!String.IsNullOrEmpty(peer))
            {
                flags.Add("peer", peer);
            }

            if (!String.IsNullOrEmpty(proto))
            {
                flags.Add("proto", proto);
            }

            if(poll)
            {
                flags.Add("poll", "true");
            }

            if (!String.IsNullOrEmpty(interval))
            {
                flags.Add("interval", interval);
            }

            HttpContent content = await ExecuteGetAsync("bw", flags, cancellationToken);

            string json = await content.ReadAsStringAsync();

            return _jsonSerializer.Deserialize<Json.IpfsStatsBw>(json);
        }
    }
}
