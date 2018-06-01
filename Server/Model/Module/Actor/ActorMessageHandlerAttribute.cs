using System;

namespace ETModel
{
	public class ActorMessageHandlerAttribute : Attribute
	{
		public AppType appType { get; }

		public ActorMessageHandlerAttribute(AppType appType)
		{
			this.appType = appType;
		}
	}
}