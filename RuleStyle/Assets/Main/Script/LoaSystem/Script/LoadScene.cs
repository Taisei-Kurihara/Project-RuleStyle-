using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{

    BitArray bit = new BitArray(4,false);

    [SerializeField]
    GameObject canvas;

    [SerializeField,Header("�t�F�[�h�C��/�A�E�g�Ɋ|���鎞��")]
    float fadeTime = 3;

    [SerializeField,Header("�t�F�[�h�C����ɃV�[����ǂݍ��ލŒ�ҋ@����")]
    float minTime = 1;

    //�t�F�[�h�C��/�A�E�g�p
    CanvasGroup canvasGroup;

    float fadetimer = 0;
    float time = 0;
    private void Awake()
    {
        canvasGroup = canvas.GetComponent<CanvasGroup>();
        canvas.SetActive(false);
    }

    /// <summary>
    /// �t�F�[�h�C�� & �A�E�g
    /// true = In / false = Out
    /// </summary>
    IEnumerator Fade(bool fade)
    {
        yield return 0;

        // �ő�l�𒴂��Ȃ��悤�ɂ��J�E���g
        fadetimer = (fadetimer + Time.deltaTime < fadeTime) ? fadetimer + Time.deltaTime : fadeTime;

        // �t�F�[�h�C��/�A�E�g�䗦�ݒ�
        float fadePerc = Mathf.Abs((fadetimer / fadeTime)  - ((fade)?0:1));

        // �A���t�@�l�ݒ�
        canvasGroup.alpha = fadePerc;

        // �I�����I���m�F
        // �����łȂ��ꍇ�J��Ԃ�
        if (fadetimer != fadeTime)
        { StartCoroutine(Fade(fade)); }
        else
        { bit[3] = fade; }
    }


    void Update()
    {
        UISceneManager loadSceneManager = UISceneManager.Instance();
        
        // ���[�h�V�[���̏�Ԃ��m�F 
        bool load = loadSceneManager.GetLoad();

        // ���[�h�V�[����؂�ւ�
        // �t�F�[�h�C�����͂����ɃA�N�e�B�u��
        // �t�F�[�h�A�E�g���̓t�F�[�h�A�E�g��ɔ�A�N�e�B�u��
        canvas.SetActive((load) ? load : bit[3]);

        // �t�F�[�h�C��/�A�E�g�J�n
        if (bit[1] != load && bit[2] != load) { bit[2] = load; fadetimer = 0; StartCoroutine(Fade(load)); }

        // �t�F�[�h�C��/�A�E�g�I�����m�F
        bit[1] = bit[3];

        // ���[�h�V�[���̏I�����m�F���^�C�}�[�n��
        time = (bit[1]) ? time + Time.deltaTime : 0;

        // ���[�h�V�[���I�����m�F
        bit[0] = (time > 1 && bit[1]) ? true : false;

        // ���[�h�V�[�����o���������Ƃ��
        if (bit[0])
        {
            loadSceneManager.SetLoad(bit[0]);
        }
    }
}
