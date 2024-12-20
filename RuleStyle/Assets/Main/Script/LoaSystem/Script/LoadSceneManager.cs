using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class LoadSceneManager : SingletonMonoBehaviourBase<LoadSceneManager>
{
    GameObject Iobj;
    string LoadSceneName = "Loadscene";


    /// <summary>
    /// 0 = LoadSceneManager���ł̒l | 1 = loadscene���ł̒l
    /// </summary>
    BitArray LoadStatus = new BitArray(2,false);

    public void SetLoad(bool xxx) { LoadStatus[1] = xxx; }
    public bool GetLoad() { return LoadStatus[0]; }


    private void Awake()
    {
        Iobj = this.gameObject;
        UnityEngine.SceneManagement.SceneManager.LoadScene(LoadSceneName, LoadSceneMode.Additive);
    }


    /// <summary>
    /// �ʓrloadscene��ύX����ꍇ
    /// </summary>
    /// <param name="key"></param>
    public void SetLoadScene(string key)
    {
        int sceneCount = SceneManager.sceneCount;

        bool Load = false;

        for (int i = 0; i < sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == LoadSceneName) { Load = true; break; }
        }

        if (Load)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(LoadSceneName);
        }
        
        LoadSceneName = key;
        UnityEngine.SceneManagement.SceneManager.LoadScene(LoadSceneName, LoadSceneMode.Additive);
    }


    /// <summary>
    /// load�J�n
    /// </summary>
    public void LoadIn(string NextSceneName, string CloseSceneName)
    {
        int sceneCount = SceneManager.sceneCount;

        bool Load = true;

        for (int i = 0; i < sceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).name == LoadSceneName) { Load = false; break; }
        }

        if (Load)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(LoadSceneName, LoadSceneMode.Additive);
        }

        LoadStatus[0] = true;

        StartCoroutine(LoadSetupWait(NextSceneName, CloseSceneName));
    }

    /// <summary>
    /// ���[�h�V�[�����N������܂ő҂�
    /// </summary>
    /// <param name="NextSceneName"></param>
    /// <param name="CloseSceneName"></param>
    /// <returns></returns>
    IEnumerator LoadSetupWait(string NextSceneName, string CloseSceneName)
    {
        yield return new WaitUntil(() => LoadStatus[1]);

        UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneName, LoadSceneMode.Additive);

        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(CloseSceneName);
    }

    /// <summary>
    /// load�I��
    /// </summary>
    public void LoadOut() { LoadStatus[0] = false; }



}
