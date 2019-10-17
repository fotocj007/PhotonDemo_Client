using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //如果鼠标点击了
        if (Input.GetMouseButtonDown(0))
        {
            SendRequest();
        }
    }

    void SendRequest()
    {
        Debug.Log("点击了------");
        
        //包装需要发送的数据
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add(1, 120);
        data.Add(2, "clent to ser");

        //向服务端发送数据,发送code是1,服务端需要用到
        PhotonManager.Peer.OpCustom(1, data, true);
    }
}
