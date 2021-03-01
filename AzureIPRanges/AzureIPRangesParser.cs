using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AzureIPRanges.Model;

namespace AzureIPRanges
{
	public class AzureIPRangesParser : IAzureIPRangesParser
	{
		public string FileName { get; private set; }
		private AzureCloudIPRanges data;

		private AzureIPRangesParser()
		{
		}

		public static async Task<AzureIPRangesParser> CreateAsync(string file, CancellationToken cancellationToken = default)
		{
			var parser = new AzureIPRangesParser
			{
				FileName = Path.GetFileName(file)
			};

			using (var stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};

				parser.data = await JsonSerializer.DeserializeAsync<AzureCloudIPRanges>(stream, options, cancellationToken);
			}
			
			return parser;
		}

		public void PrintServiceTagIds()
		{
			foreach (var item in this.data.Values.Select(x => new {x.Id, x.Name}))
			{
				Console.WriteLine($"{item.Id}");
			}
		}

		public IEnumerable<IPNetwork> GetIPNetworksForServiceTag(string serviceTagId, AddressFamily[] addressFamilies = null)
		{
			var addressPrefixes = this.data.Values.FirstOrDefault(x => x.Id.Equals(serviceTagId, StringComparison.InvariantCultureIgnoreCase))?.Properties?.AddressPrefixes ?? Array.Empty<string>();

			var result = new List<IPNetwork>(addressPrefixes.Length);

			foreach (var addressPrefix in addressPrefixes)
			{
				if (!IPNetwork.TryParse(addressPrefix, out var ipNetwork))
				{
					Console.WriteLine($"Failed to parse address prefix {addressPrefix}");
				}

				if (addressFamilies == null || addressFamilies.Contains(ipNetwork.AddressFamily))
				{
					result.Add(ipNetwork);
				}
			}

			return result;
		}

		public IEnumerable<string> GetServiceTagsForIPAddress(IPAddress ipAddress)
		{
			foreach (var serviceTag in this.data.Values)
			{
				var ipNetworksForServiceTag = GetIPNetworksForServiceTag(serviceTag.Id);

				foreach (var ipNetwork in ipNetworksForServiceTag)
				{
					if (ipNetwork.Contains(ipAddress))
					{
						yield return serviceTag.Id;
						break;
					}
				}
			}
		}
	}
}