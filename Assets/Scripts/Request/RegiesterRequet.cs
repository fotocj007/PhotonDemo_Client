using System.Collections;
using System.Collections.Generic;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public class RegiesterRequet : Request
{
    [HideInInspector]
    public string username; 
    [HideInInspector]
    public string password;

    private ReginsterPanel reginsterPanel;

    public override void Start()
    {
        base.Start();
        reginsterPanel = GetComponent<ReginsterPanel>();
    }

    public override void DefaultRequest()
    {
        //登录请求发起
        Dictionary<byte,object> data = new Dictionary<byte, object>();
        data.Add((byte) ParameterCode.UserName,username);
        data.Add((byte) ParameterCode.Password,password);
        
        PhotonManager.Peer.OpCustom((byte)OpCode,data,true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        ReturnCode returnCode = (ReturnCode) operationResponse.ReturnCode;
        reginsterPanel.OnReigsterResponse(returnCode);
    }
    
}
