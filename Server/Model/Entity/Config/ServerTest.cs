namespace ETModel
{
	[Config(AppType.Client)]
	public partial class ServerTestCategory : ACategory<ServerTest>
	{
	}

	public class ServerTest: IConfig
	{
		public long Id { get; set; }
		public string Name;
		public string Desc;
		public int Position;
		public int Height;
		public int Weight;
	}
}
