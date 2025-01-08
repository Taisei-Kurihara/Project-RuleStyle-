using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadOut : MonoBehaviour
{


    // objectの有効化時
    private void OnEnable()
    {
        UISceneManager uISceneManager = UISceneManager.Instance();
        uISceneManager.LoadOut();
    }
}
