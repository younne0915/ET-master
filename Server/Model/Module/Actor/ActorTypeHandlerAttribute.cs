using System;

namespace ETModel
{
	public class ActorTypeHandlerAttribute : Attribute
	{
		public AppType appType { get; }

		public string actorType { get; }

		public ActorTypeHandlerAttribute(AppType appType, string actorType)
		{
			this.appType = appType;
			this.actorType = actorType;
		}
	}
}