using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Toos;
using ExitGames.Client.Photon;
using UnityEngine;

public class SyncPlayerRequest : Request
{
    private Player player;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void DefaultRequest()
    {
        //请求服务端,获取其他玩家信息
        PhotonManager.Peer.OpCustom((byte)OpCode,null,true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
//        List<string> userLists = (List<string>)DictTool.GetValue(operationResponse.Parameters, (byte)ParameterCode.UsernameList);

        string userString = (string) DictTool.GetValue(operationResponse.Parameters, (byte) ParameterCode.UsernameList);

        //解析,反序列化
        using (StringReader reader = new StringReader(userString))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            List<string> usernameList = (List<string>)serializer.Deserialize(reader);
            
            player.OnSysPlayerRequest(usernameList);
        }
        
    }
}
