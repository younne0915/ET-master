namespace ETModel
{
	[Config(AppType.Client)]
	public partial class HeroCategory : ACategory<Hero>
	{
	}

	public class Hero: IConfig
	{
		public long Id { get; set; }
		public string Name;
		public int Sex;
		public float HP;
		public float MP;
		public float ViewDis;
		public float AttackDis;
		public float Attack;
		public float Deffense;
		public string ModelName;
	}
}
