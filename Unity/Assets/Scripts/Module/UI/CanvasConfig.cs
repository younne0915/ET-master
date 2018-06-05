using UnityEngine;

namespace ETModel
{ 
    public static class CanvasType
    {
        public const string LoginCanvas = "LoginCanvas";
        public const string LobbyCanvas = "LobbyCanvas";
    }

    public class CanvasConfig: MonoBehaviour
	{
		public string CanvasName;
	}
}
