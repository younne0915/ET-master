using System.Threading.Tasks;

namespace ETModel
{
	public struct ActorTask
	{
		public IActorLanuch ActorLaunch;
		
		public TaskCompletionSource<IResponse> Tcs;

		public ActorTask(IActorLanuch actorLaunch)
		{
			this.ActorLaunch = actorLaunch;
			this.Tcs = null;
		}
		
		public ActorTask(IActorLanuch actorLaunch, TaskCompletionSource<IResponse> tcs)
		{
			this.ActorLaunch = actorLaunch;
			this.Tcs = tcs;
		}
	}
}