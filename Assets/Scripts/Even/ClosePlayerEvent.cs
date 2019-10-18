using Common;
using Common.Toos;
using ExitGames.Client.Photon;

namespace Even
{
    public class ClosePlayerEvent:BaseEvent
    {
        private Player player;

        public override void Start()
        {
            base.Start();
            player = GetComponent<Player>();
        }
        
        public override void OnEvent(EventData eventData)
        {
            string username = (string)DictTool.GetValue(eventData.Parameters, (byte) ParameterCode.UserName);
            player.OnClosePlayer(username);
        }
    }
}