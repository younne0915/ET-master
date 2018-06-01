using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
	[ActorTypeHandler(AppType.AllServer, ActorType.Common)]
	public class CommonActorTypeHandler : IActorTypeHandler
	{
		public async Task Handle(Session session, Entity entity, IActorLanuch actorMessage)
		{
			await Game.Scene.GetComponent<ActorMessageDispatherComponent>().Handle(session, entity, actorMessage);
		}
	}
}
