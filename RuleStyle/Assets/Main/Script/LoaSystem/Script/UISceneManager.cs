using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Taskを使うために必要




/// <summary>
/// SingletonStaticクラス
/// </summary>
public class UISceneManager : SingletonMonoBehaviourBase<UISceneManager>
{

    private void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(LoadSceneName.ToString(), LoadSceneMode.Additive);
    }

    #region UI
    // 現在出現しているUI
    BitArray nowUIAdvents = new BitArray(32,false);

    // UI一覧(手書き)
    private Call[] uinames = new Call[] {  };

    // 出現しているUIの一覧
    List<Call> lastUIScene = new List<Call>();

    // 一番最後のMaine(タイトルやゲームscene)scene
    public Call lastMainScene = Call.TitleTest;

    void Advent(Call call)
    {
        TimeStop();
        uiAdvent(call);
        UnityEngine.SceneManagement.SceneManager.LoadScene(call.ToString(), LoadSceneMode.Additive);
    }

    void Delete(Call call)
    {
        uiDelete();
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(call.ToString());
    }


    #region Call ( 0 )
    public void CallAdvent(Call call)
    {
        if (call == Call.Now) { call = GetLast(); }
        for (int i = 0; i < uinames.Length; i++)
        {
            if (!nowUIAdvents[i])
            {
                nowUIAdvents[i] = true;
                Advent(call);
                return;
            }
        }
    }

    public void CallDelete(Call call)
    {
        if (call == Call.Now) { call = GetLast(); }
        for (int i = 0; i < uinames.Length; i++)
        {
            if (call == uinames[i]) {
                
                Debug.Log("Del");
                nowUIAdvents[i] = false;
                Delete(call);

                return;
            } 
        }


        
    }
    #endregion

    /// <summary>
    /// 1:true:停止/faluse:流れる
    /// </summary>
    private BitArray TimeChange = new BitArray(1, false);

    void TimeStop() { TimeChange[0] = true; Time.timeScale = 0; }
    void TimeReStart() { TimeChange[0] = false; Time.timeScale = 1; }
    public bool GetTimeChange() { return TimeChange[0]; }

    Call GetLast()
    {
        if(lastUIScene.Count > 0) { return lastUIScene[lastUIScene.Count - 1]; }
        return lastMainScene;
    }

    public void uiAdvent(Call call) { lastUIScene.Add(call); }
    public void uiDelete()
    {
        List<Call> list = lastUIScene;
        lastUIScene.Clear();
        for (int i = 0; i < list.Count - 1; i++)
        {
            lastUIScene.Add(list[i]);
        }

        if (lastUIScene.Count <= 0) { TimeReStart(); }
    }

    public void ResetScene()
    {
        lastUIScene.Clear();
        TimeReStart();
        nowUIAdvents = new BitArray(32, false);
    }

    #endregion

    //------------------------------------------------------------------------------------------------------------------------------------------------------
    //
    //------------------------------------------------------------------------------------------------------------------------------------------------------

    #region ロードシーン

    Call LoadSceneName = Call.Loadscene;


    /// <summary>
    /// 0 = LoadSceneManager内での値 | 1 = loadscene内での値
    /// </summary>
    BitArray LoadStatus = new BitArray(2, false);

    public void SetLoad(bool xxx) { LoadStatus[1] = xxx; }
    public bool GetLoad() { return LoadStatus[0]; }


    /// <summary>
    /// 別途loadsceneを変更する場合
    /// </summary>
    /// <param name="key"></param>
    public void SetLoadScene(Call key)
    {
        int sceneCount = SceneManager.sceneCount;

        bool Load = false;

        for (int i = 0; i < sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == LoadSceneName.ToString()) { Load = true; break; }
        }

        if (Load)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(LoadSceneName.ToString());
        }

        LoadSceneName = key;
        UnityEngine.SceneManagement.SceneManager.LoadScene(LoadSceneName.ToString(), LoadSceneMode.Additive);
    }


    /// <summary>
    /// load開始
    /// </summary>
    public void LoadIn(Call NextSceneName, Call CloseSceneName)
    {

        int sceneCount = SceneManager.sceneCount;

        bool Load = true;

        for (int i = 0; i < sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == LoadSceneName.ToString()) { Load = false; break; }
        }

        if (Load)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(LoadSceneName.ToString(), LoadSceneMode.Additive);
        }

        LoadStatus[0] = true;

        if(CloseSceneName == Call.Now) { CloseSceneName = lastMainScene; }

        StartCoroutine(LoadSetupWait(NextSceneName, CloseSceneName));
    }

    /// <summary>
    /// ロードシーンが起動したことを確認するまで待つ
    /// </summary>
    /// <param name="NextSceneName"></param>
    /// <param name="CloseSceneName"></param>
    /// <returns></returns>
    IEnumerator LoadSetupWait(Call NextSceneName, Call CloseSceneName)
    {
        yield return new WaitUntil(() => LoadStatus[1]);

        UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneName.ToString(), LoadSceneMode.Additive);

        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(CloseSceneName.ToString());
    }

    /// <summary>
    /// load終了指示
    /// </summary>
    public void LoadOut()
    {
        //ロード状態のリセット
        LoadStatus = new BitArray(2, false);
    }

    #endregion
}

