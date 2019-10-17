using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Toos;
using ExitGames.Client.Photon;
using UnityEngine;

public class NewPlayerEvent : BaseEvent
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
        player.OnNewPlayerEvent(username);
    }
}
