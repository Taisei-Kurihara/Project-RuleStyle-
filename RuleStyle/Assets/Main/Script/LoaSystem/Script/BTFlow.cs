using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum CallType
{
    addUI,
    closeUI,
    changescene,
    Quit
}
public enum Call
{
    None,
    Now,

    //scene
    Title,
    TitleTest,
    Game,
    GameTest,

    //UIscene
    Loadscene
}

/// <summary>
/// sceneˆÚ“®/‰ÁŽZˆÈŠO‚ÌŒø‰Ê
/// </summary>
public enum Extra
{
    OnPose,
    OffPose
}
public class BTFlow : MonoBehaviour
{
    [SerializeField]
    CallType callType;
    [SerializeField]
    Call call;

    [SerializeField]
    Extra[] extra;

    Button thisButton;
    private void Awake()
    {
        thisButton = GetComponent<Button>();
        EventExecution();
    }

    public void EventExecution() { thisButton.onClick.AddListener(Onclick); }
    public void EventUnExecuted() { thisButton.onClick.RemoveListener(Onclick); }


    public void Onclick()
    {
        Debug.Log(this.gameObject.name);
        for (int i = 0; i < extra.Length; i++)
        {
            switch (extra[i])
            {
                case Extra.OnPose:
                    Time.timeScale = 0;
                    break;
                case Extra.OffPose:
                    Time.timeScale = 1;
                    break;
            }
        }


        UISceneManager uISceneManager = UISceneManager.Instance();

        switch (callType)
        {
            case CallType.addUI:
                uISceneManager.CallAdvent(call);
                break;
            case CallType.closeUI:
                uISceneManager.CallDelete(call);
                break;
            case CallType.changescene:
                Call call2 = uISceneManager.lastMainScene;
                uISceneManager.lastMainScene = call;
                uISceneManager.ResetScene();
                uISceneManager.LoadIn(call, call2);
                break;
            case CallType.Quit:
                Application.Quit();
                break;
        }
    }

}
