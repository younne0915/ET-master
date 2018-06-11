namespace ETModel
{
	[Config(AppType.Client)]
	public partial class PublicTestCategory : ACategory<PublicTest>
	{
	}

	public class PublicTest: IConfig
	{
		public long Id { get; set; }
		public string Name;
		public string Desc;
		public int Position;
		public int Height;
		public int Weight;
	}
}
