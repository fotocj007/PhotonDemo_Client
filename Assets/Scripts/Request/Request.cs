using System;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public abstract class Request: MonoBehaviour
{
    //定义请求code
    public OperationCode OpCode;
    
    public abstract void DefaultRequest(); //发送请求
    
    public abstract void OnOperationResponse(OperationResponse operationResponse); //接收返回的数据

    public virtual void Start()
    {
        //从管理类中添加该请求
        PhotonManager.Instance.AddRequest(this);
    }

    public void OnDestroy()
    {
        //从管理类中移除该请求
        PhotonManager.Instance.RemoveRequest(this);
    }
}
