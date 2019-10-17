
using System.Collections.Generic;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public class LoginRequest : Request
{
    //通过代码赋值,所有在UI界面上隐藏掉
    [HideInInspector]
    public string UserName;
    [HideInInspector]
    public string Password;

    //获取UI面板
    private LoginPanel loginPanel;

    public override void Start()
    {
        //调用基类Start,将登陆请求添加到管理类中
        base.Start();
        loginPanel = GetComponent<LoginPanel>();
    }
    public override void DefaultRequest()
    {
        //登录请求发起
        Dictionary<byte,object> data = new Dictionary<byte, object>();
        data.Add((byte) ParameterCode.UserName,UserName);
        data.Add((byte) ParameterCode.Password,Password);
        
        //通过Peer发送请求给服务端
        PhotonManager.Peer.OpCustom((byte)OpCode,data,true);
    }
    
    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        //接收服务端的返回响应,将返回code给面板,由面板来处理接下来的逻辑(是提示错误还是跳转场景)
        ReturnCode returnCode = (ReturnCode) operationResponse.ReturnCode;

        if (returnCode == ReturnCode.Success)
        {
            PhotonManager.username = UserName;
        }
        
        loginPanel.OnLiginResponse(returnCode);
    }
}
