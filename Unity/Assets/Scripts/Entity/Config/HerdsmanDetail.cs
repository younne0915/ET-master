namespace ETModel
{
	[Config(AppType.Client)]
	public partial class HerdsmanDetailCategory : ACategory<HerdsmanDetail>
	{
	}

	public class HerdsmanDetail: IConfig
	{
		public long Id { get; set; }
		public string Name;
		public int Sex;
		public int Position;
		public int Height;
		public int Weight;
	}
}
