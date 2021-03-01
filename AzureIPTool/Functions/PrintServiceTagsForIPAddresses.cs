using System;
using System.Net;
using AzureIPRanges;

namespace AzureIPTool.Functions
{
	public class PrintServiceTagsForIPAddresses : IAzureIPToolFunction
	{
		private readonly IAzureIPRangesParser parser;
		private readonly string[] ipAddresses;

		public PrintServiceTagsForIPAddresses(IAzureIPRangesParser parser, string[] ipAddresses)
		{
			this.parser = parser;
			this.ipAddresses = ipAddresses;
		}

		public void Execute()
		{
			foreach (var ipAddressString in this.ipAddresses)
			{
				if (!IPAddress.TryParse(ipAddressString, out var ipAddress))
				{
					Console.WriteLine($"Couldn't parse IP address '{ipAddressString}'");
					continue;
				}

				Console.WriteLine(ipAddress);

				foreach (var serviceTagId in this.parser.GetServiceTagsForIPAddress(ipAddress))
				{
					Console.WriteLine($"- {serviceTagId}");
				}
			}
		}
	}
}