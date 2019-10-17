using System.Collections;
using System.Collections.Generic;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public class SyncPositionRequest : Request
{
    [HideInInspector]
    public Vector3 pos;
    
    public override void DefaultRequest()
    {
        //登录请求发起
        Dictionary<byte,object> data = new Dictionary<byte, object>();
//        data.Add((byte) ParameterCode.Position, new Vector3Data(pos.x, pos.y, pos.z));
        data.Add((byte) ParameterCode.X, pos.x);
        data.Add((byte) ParameterCode.Y, pos.y);
        data.Add((byte) ParameterCode.Z, pos.z);
        
        //通过Peer发送请求给服务端
        PhotonManager.Peer.OpCustom((byte)OpCode,data,true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        throw new System.NotImplementedException();
    }
}
