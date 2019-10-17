using System.Collections;
using System.Collections.Generic;
using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public abstract class BaseEvent : MonoBehaviour
{
    public EventCode EventCode;
    public abstract void OnEvent(EventData eventData);
    
    // Start is called before the first frame update
    public  virtual void Start()
    {
        PhotonManager.Instance.AddEvent(this);
    }

    // Update is called once per frame
    public void OnDestroy()
    {
        PhotonManager.Instance.RemoveEvent(this);
    }
}
