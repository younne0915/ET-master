using System;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
	/// <summary>
	/// gate session收到的actor消息直接转发给客户端
	/// </summary>
	[ActorTypeHandler(AppType.Gate, ActorType.GateSession)]
	public class GateSessionActorTypeHandler : IActorTypeHandler
	{
		public async Task Handle(Session session, Entity entity, IActorLanuch actorLaunch)
		{
			ActorResponse actorResponse = new ActorResponse
			{
				RpcId = actorLaunch.RpcId
			};
			try
			{
				// 发送给客户端
				Session clientSession = entity as Session;
				actorLaunch.ActorId = 0;
				clientSession.Send(actorLaunch);

				session.Reply(actorResponse);
				await Task.CompletedTask;
			}
			catch (Exception e)
			{
				actorResponse.Error = ErrorCode.ERR_SessionActorError;
				actorResponse.Message = $"session actor error {e}";
				session.Reply(actorResponse);
				throw;
			}
		}
	}
}
