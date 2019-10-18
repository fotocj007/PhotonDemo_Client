using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Toos;
using UnityEngine;

public class Player : MonoBehaviour
{
    //当前的玩家
    public bool isLocalPlayer = true;

    private SyncPositionRequest syncPosRequest;

    private string username;
    //记录上次位置
    private Vector3 lastPos = Vector3.zero;
    private float moveOff = 1.0f;
    
    private Dictionary<string,GameObject> playerDic = new Dictionary<string, GameObject>();

    private SyncPlayerRequest syncPlayerRequest;

    public GameObject playerPrefab;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Renderer>().material.color = Color.green;

        syncPosRequest = GetComponent<SyncPositionRequest>();
        syncPlayerRequest = GetComponent<SyncPlayerRequest>();
       
        syncPlayerRequest.DefaultRequest();
           
       //定时调用某个函数
        InvokeRepeating("SyncPosition",1,moveOff);
    }
    
    //同步位置函数
    void SyncPosition()
    {
        if (Vector3.Distance(player.transform.position, lastPos) > 0.1f)
        {
            var position = player.transform.position;
            lastPos = position;
            syncPosRequest.pos = position;
            syncPosRequest.DefaultRequest();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
            
        player.transform.Translate(4 * Time.deltaTime * new Vector3(h,0,v));
    }

    public void OnSysPlayerRequest(List<string> userList)
    {
        //创建其他客户端player角色
        foreach (var tmpname in userList)
        {
            OnNewPlayerEvent(tmpname);
        }
    }

    public void OnNewPlayerEvent(string name)
    {
        var isOk = DictTool.GetValue(playerDic, name);
        if (isOk == null)
        {
            GameObject gos = GameObject.Instantiate(playerPrefab);
            playerDic.Add(name,gos);
        }
    }

    //有用户退出时销毁用户,并从管理类中移除
    public void OnClosePlayer(string name)
    {
        GameObject CloseP = DictTool.GetValue(playerDic, name);
        Destroy(CloseP);
        playerDic.Remove(name);
    }

    public void OnSyncPositionEvent(List<PlayerData> playerDatas)
    {
        foreach (PlayerData pd in playerDatas)
        {
            GameObject go = DictTool.GetValue(playerDic, pd.Username);
            if(go != null)
                go.transform.position = new Vector3(){x=pd.Pos.x, y=pd.Pos.y, z=pd.Pos.z};
        }
    }
}
