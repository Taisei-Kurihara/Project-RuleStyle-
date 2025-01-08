using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Task���g�����߂ɕK�v




/// <summary>
/// SingletonStatic�N���X
/// </summary>
public class UISceneManager : SingletonMonoBehaviourBase<UISceneManager>
{

    private void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(LoadSceneName.ToString(), LoadSceneMode.Additive);
    }

    #region UI
    // ���ݏo�����Ă���UI
    BitArray nowUIAdvents = new BitArray(32,false);

    // UI�ꗗ(�菑��)
    private Call[] uinames = new Call[] {  };

    // �o�����Ă���UI�̈ꗗ
    List<Call> lastUIScene = new List<Call>();

    // ��ԍŌ��Maine(�^�C�g����Q�[��scene)scene
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
    /// 1:true:��~/faluse:�����
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

    #region ���[�h�V�[��

    Call LoadSceneName = Call.Loadscene;


    /// <summary>
    /// 0 = LoadSceneManager���ł̒l | 1 = loadscene���ł̒l
    /// </summary>
    BitArray LoadStatus = new BitArray(2, false);

    public void SetLoad(bool xxx) { LoadStatus[1] = xxx; }
    public bool GetLoad() { return LoadStatus[0]; }


    /// <summary>
    /// �ʓrloadscene��ύX����ꍇ
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
    /// load�J�n
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
    /// ���[�h�V�[�����N���������Ƃ��m�F����܂ő҂�
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
    /// load�I���w��
    /// </summary>
    public void LoadOut()
    {
        //���[�h��Ԃ̃��Z�b�g
        LoadStatus = new BitArray(2, false);
    }

    #endregion
}

