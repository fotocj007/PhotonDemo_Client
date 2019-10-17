using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Toos;
using ExitGames.Client.Photon;
using UnityEngine;

public class SyncPositionEvent : BaseEvent
{
    private Player player;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void OnEvent(EventData eventData)
    {
        string playerDataS = (string)DictTool.GetValue(eventData.Parameters, (byte)ParameterCode.PlayerDataList);

        using (StringReader reader = new StringReader(playerDataS))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerData>));

            List<PlayerData> playerDates = (List<PlayerData>)serializer.Deserialize(reader);
            player.OnSyncPositionEvent(playerDates);
        }
    }
}
