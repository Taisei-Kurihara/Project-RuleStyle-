using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{

    BitArray bit = new BitArray(4,false);

    [SerializeField]
    GameObject canvas;

    [SerializeField,Header("フェードイン/アウトに掛かる時間")]
    float fadeTime = 3;

    [SerializeField,Header("フェードイン後にシーンを読み込む最低待機時間")]
    float minTime = 1;

    //フェードイン/アウト用
    CanvasGroup canvasGroup;

    float fadetimer = 0;
    float time = 0;
    private void Awake()
    {
        canvasGroup = canvas.GetComponent<CanvasGroup>();
        canvas.SetActive(false);
    }

    /// <summary>
    /// フェードイン & アウト
    /// true = In / false = Out
    /// </summary>
    IEnumerator Fade(bool fade)
    {
        yield return 0;

        // 最大値を超えないようにしつつカウント
        fadetimer = (fadetimer + Time.deltaTime < fadeTime) ? fadetimer + Time.deltaTime : fadeTime;

        // フェードイン/アウト比率設定
        float fadePerc = Mathf.Abs((fadetimer / fadeTime)  - ((fade)?0:1));

        // アルファ値設定
        canvasGroup.alpha = fadePerc;

        // 終了時終了確認
        // そうでない場合繰り返す
        if (fadetimer != fadeTime)
        { StartCoroutine(Fade(fade)); }
        else
        { bit[3] = fade; }
    }


    void Update()
    {
        UISceneManager loadSceneManager = UISceneManager.Instance();
        
        // ロードシーンの状態を確認 
        bool load = loadSceneManager.GetLoad();

        // ロードシーンを切り替え
        // フェードイン時はすぐにアクティブ化
        // フェードアウト時はフェードアウト後に非アクティブ化
        canvas.SetActive((load) ? load : bit[3]);

        // フェードイン/アウト開始
        if (bit[1] != load && bit[2] != load) { bit[2] = load; fadetimer = 0; StartCoroutine(Fade(load)); }

        // フェードイン/アウト終了を確認
        bit[1] = bit[3];

        // ロードシーンの終了を確認時タイマー始動
        time = (bit[1]) ? time + Time.deltaTime : 0;

        // ロードシーン終了を確認
        bit[0] = (time > 1 && bit[1]) ? true : false;

        // ロードシーンが出現したことを報告
        if (bit[0])
        {
            loadSceneManager.SetLoad(bit[0]);
        }
    }
}
