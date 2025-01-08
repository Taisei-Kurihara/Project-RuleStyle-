using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadOut : MonoBehaviour
{


    // object‚Ì—LŒø‰»Žž
    private void OnEnable()
    {
        UISceneManager uISceneManager = UISceneManager.Instance();
        uISceneManager.LoadOut();
    }
}
