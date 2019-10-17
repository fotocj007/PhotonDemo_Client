using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.UI;

public class ReginsterPanel : MonoBehaviour
{
    public GameObject loginPanel;

    public InputField username;
    public InputField password;
    public Text hintmessage;

    private RegiesterRequet regiesterRequet;

    private void Start()
    {
        regiesterRequet = GetComponent<RegiesterRequet>();
    }

    public void OnRegisterButton()
    {
        hintmessage.text = "";
            
        regiesterRequet.username = username.text;
        regiesterRequet.password = password.text;
        
        regiesterRequet.DefaultRequest();
    }
    
    public void OnBackBtn()
    {
        loginPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnReigsterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            hintmessage.text = "注册成功,请返回登录";
        }
        else
        {
            hintmessage.text = "用户名重复,请重新修改用户名";
        }
    }
}
