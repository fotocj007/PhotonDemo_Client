using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Common;
using Common.Toos;
using UnityEngine;
using ExitGames.Client.Photon;

public class PhotonManager : MonoBehaviour,IPhotonPeerListener
{
    //单例模式,保证只有一个链接服务器
    public static PhotonManager Instance;

    private static PhotonPeer peer;
    private bool connected;
    
    private Dictionary<OperationCode,Request> ResquesDict = new Dictionary<OperationCode, Request>();
    private Dictionary<EventCode,BaseEvent> EventDict = new Dictionary<EventCode, BaseEvent>();

    public static string username;
    
    public static PhotonPeer Peer
    {
        get { return peer; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("111------开始连接----");
        
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        peer.Connect("47.92.119.159:5055","MyGame1");
        connected = false;
    }

    // Update is called once per frame
    void Update()
    {
        peer.Service();
    }

    private void OnDestroy()
    {
        if (peer != null && peer.PeerState == PeerStateValue.Connected)
        {
            peer.Disconnect();
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
    }

    /// <summary>
    /// 客户端请求后,服务端响应,返回数据
    /// </summary>
    /// <param name="operationResponse"></param>
    public void OnOperationResponse(OperationResponse operationResponse)
    {
        //服务端返回的code
        OperationCode opCode = (OperationCode)operationResponse.OperationCode;
        Request request = null;
        
        //根据code查找相应请求
        bool tmp = ResquesDict.TryGetValue(opCode, out request);
        
        if (tmp)
        {
            //如果存在,分发给相应请求去接收服务端数据进行处理
            request.OnOperationResponse(operationResponse);
        }
        else
        {
            Debug.Log("没找到响应处理对象");
        }
    }

    /// <summary>
    /// 连接状态发生改变时
    /// </summary>
    /// <param name="statusCode"></param>
    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                connected = true;
                break;
            default:
                connected = false;
                break;
        }
    }

    /// <summary>
    /// 服务端直接给客户端数据时,不需要向服务器请求
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEvent(EventData eventData)
    {
        EventCode code = (EventCode)eventData.Code;
        BaseEvent e = DictTool.GetValue(EventDict, code);
        e.OnEvent(eventData);
    }

    public void AddRequest(Request rest)
    {
        ResquesDict.Add(rest.OpCode,rest);
    }

    public void RemoveRequest(Request rest)
    {
        ResquesDict.Remove(rest.OpCode);
    }
    
    public void AddEvent(BaseEvent even)
    {
        EventDict.Add(even.EventCode,even);
    }

    public void RemoveEvent(BaseEvent even)
    {
        EventDict.Remove(even.EventCode);
    }
}
