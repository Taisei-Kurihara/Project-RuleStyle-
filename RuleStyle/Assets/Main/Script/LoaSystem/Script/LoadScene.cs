using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{

    BitArray bit = new BitArray(2,false);

    [SerializeField]
    GameObject canvas;

    float time = 0;

    private void Awake()
    {
        canvas.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        LoadSceneManager loadSceneManager = LoadSceneManager.Instance();

        if (time > 1 && bit[1])
        {
            bit[0] = true;
            loadSceneManager.SetLoad(bit[0]);
        }


        bit[1] = loadSceneManager.GetLoad();

        if (bit[1])
        {
            canvas.SetActive(true);
            time += Time.deltaTime;
        }
        else
        {
            canvas.SetActive(false);
            time = 0;
        }
    }
}
