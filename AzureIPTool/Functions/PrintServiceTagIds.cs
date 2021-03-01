using AzureIPRanges;

namespace AzureIPTool.Functions
{
	public class PrintServiceTagIds : IAzureIPToolFunction
	{
		private readonly IAzureIPRangesParser parser;

		public PrintServiceTagIds(IAzureIPRangesParser parser)
		{
			this.parser = parser;
		}

		public void Execute()
		{
			this.parser.PrintServiceTagIds();
		}
	}
}