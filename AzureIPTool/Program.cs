using System;
using System.Threading.Tasks;
using AzureIPRanges;
using AzureIPTool.Functions;

namespace AzureIPTool
{
	public static class Program
	{
		public static async Task<int> Main(string[] args)
		{
			if (args.Length < 2)
			{
				PrintUsage();
				return -1;
			}

			var file = args[0];
			var function = args[1].ToLowerInvariant();

			var parser = await AzureIPRangesParser.CreateAsync(file);

			switch (function)
			{
				case "servicetagids":
					ExecuteFunction(new PrintServiceTagIds(parser));
					break;
				case "openvpnroutes":
					ExecuteFunction(new OpenVPNRoutes(parser, args[2..]));
					break;
				case "servicetagsforips":
					ExecuteFunction(new PrintServiceTagsForIPAddresses(parser, args[2..]));
					break;
				default:
					Console.WriteLine($"Unknown function '{function}'");
					PrintUsage();
					return -1;
			}

			return 0;
		}

		// https://docs.microsoft.com/en-us/azure/virtual-network/service-tags-overview

		// Some relevant service tags:
		// AppService{.region}			-- doesn't include App Service Environments, they're just part of AzureCloud{.region}
		// AzureKeyVault{.region}
		// Sql{.region}
		// EventHub.{region}			-- use for ServiceBus below premium tier
		// ServiceBus{.region}			-- premium tier only, use EventHub for lower tiers
		// Storage{.region}
		// AzureCloud{.region}			-- all public IPs for cloud region

		private static void PrintUsage()
		{
			Console.WriteLine("Usage:");
			Console.WriteLine("- AzureIPTool.exe <path to service tags json file> <function> [<function options>]");
			Console.WriteLine();
			Console.WriteLine("Functions:");
			Console.WriteLine("- servicetagids");
			Console.WriteLine("- openvpnroutes <service tag id 1> ... <service tag id n>");
			Console.WriteLine("- servicetagsforips <ip address 1> ... <ip address n>");
			Console.WriteLine();
			Console.WriteLine("e.g.");
			Console.WriteLine("- AzureIPTool.exe ServiceTags_Public_20210111.json openvpnroutes Sql.WestUS2 Storage.WestUS2");
			Console.WriteLine("- AzureIPTool.exe ServiceTags_Public_20210111.json servicetagsforips 13.66.226.80 13.77.157.133 20.36.24.143 13.66.226.202");
		}

		private static void ExecuteFunction(IAzureIPToolFunction function)
		{
			function.Execute();
		}
	}
}
