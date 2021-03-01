using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace AzureIPRanges
{
	public interface IAzureIPRangesParser
	{
		string FileName { get; }
		void PrintServiceTagIds();
		IEnumerable<IPNetwork> GetIPNetworksForServiceTag(string serviceTagId, AddressFamily[] addressFamilies = null);
		IEnumerable<string> GetServiceTagsForIPAddress(IPAddress ipAddress);
	}
}