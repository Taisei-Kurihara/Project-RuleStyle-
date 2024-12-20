using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOut : MonoBehaviour
{

    private void OnEnable()
    {
        UISceneManager uISceneManager = UISceneManager.Instance();
        uISceneManager.LoadOut();
    }
}
