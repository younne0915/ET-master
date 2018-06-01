using ProtoBuf;

// 不要在这个文件加[ProtoInclude]跟[BsonKnowType]标签,加到InnerMessage.cs或者OuterMessage.cs里面去
namespace ETModel
{
	public interface IActorLanuch: IRequest
	{
		long ActorId { get; set; }
	}

    [ProtoContract]
    public interface IActorNotify : IActorLanuch
    {
    }

    [ProtoContract]
	public interface IActorRequest : IActorLanuch
	{
	}

	[ProtoContract]
	public interface IActorResponse : IResponse
	{
	}

	[ProtoContract]
	public interface IFrameMessage : IMessage
	{
		long Id { get; set; }
	}
}