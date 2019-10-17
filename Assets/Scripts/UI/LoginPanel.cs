using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    //以下public参数通过UI拖拽进行赋值
    #region Tags
    
    //注册面板,用于点击注册时显示该面板
    public GameObject registerPanel;
    //控件
    public InputField username;
    public InputField password;
    public Text hintmessage;

    //登陆请求类
    private LoginRequest loginRequest;
    
    #endregion
   
    private void Start()
    {
        //从面板中获取请求类(面板脚本和请求类都挂载到请求面板UI中)
        loginRequest = GetComponent<LoginRequest>();
    }

    public void OnLoginButton()
    {
        //点击登陆按钮时,发送请求
        hintmessage.text = "";
        
        loginRequest.UserName = username.text;
        loginRequest.Password = password.text;
        
        //调用请求类中的发起请求
        loginRequest.DefaultRequest();
    }

    public void OnRegisterButton()
    {
        //点击注册时,隐藏登陆UI,显示注册UI
        gameObject.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void OnLiginResponse(ReturnCode returnCode)
    {
        //服务端响应返回数据时,请求类会将返回信息传递到该函数
        Debug.Log("-----返回code" + returnCode);
        
        if (returnCode == ReturnCode.Success)
        {
            //登录成功,调整下一个场景
            Debug.Log("--登陆成功----");

            SceneManager.LoadScene("Game");
        }
        else
        {
            hintmessage.text = "用户名或密码错误";
        }
    }
}
