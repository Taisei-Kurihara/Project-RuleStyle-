using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager :SingletonMonoBehaviourBase<GameManager>
{

    [SerializeField]
    public int PlayerNum=0;

    /// <summary>
    /// �v���C���[�̐l��
    /// </summary>
    public PlayerData playerData_One=null;
    public PlayerData playerData_Two=null;
    public PlayerData playerData_Three = null;
    public PlayerData playerData_Four = null;


    public Dictionary<int, PlayerData> Key_playerdata;

    public List<int> Number=null;

    /// <summary>
    /// �N���A����ׂ̓_��
    /// </summary>
    public int ClearPoint=0;
    /// <summary>
    /// �v���C���[�̐l������
    /// </summary>
    /// <param name="_playernum"></param>
    public void GameInit(int _playernum)
    {
        //�L�[�̃f�[�^�}��
        Key_playerdata=new Dictionary<int, PlayerData> {
            {1,playerData_One },
            {2,playerData_Two },
            {3,playerData_Three },
            {4,playerData_Four }
        };
       
        //�v���C���[�̃f�[�^�̏�����
        //���΂ɐl�����ɍ��̂͂�낵���Ȃ��̂ŁB
        switch (_playernum)
        {
            case 2:
                Number =new List<int>{1,2};

                playerData_One = new PlayerData();
                playerData_Two = new PlayerData();
                break;
            case 3:
                Number = new List<int> { 1, 2 ,3};

                playerData_One = new PlayerData();
                playerData_Two = new PlayerData();
                playerData_Three= new PlayerData();
                break;
            case 4:
                Number = new List<int> { 1, 2, 3 };

                playerData_One = new PlayerData();
                playerData_Two = new PlayerData();
                playerData_Three = new PlayerData();
                playerData_Four= new PlayerData();
                break;
            default:
                Debug.Log("�G���[���o�Ă��܂�(�v���C���[�̐l���ُ̈�)");
                break;
        }
    }

    /// <summary>
    /// ���ԃV���b�t��
    /// </summary>
    public void Shuffle(List<int> array)
    {
        for (var i = array.Count - 1; i > 0; --i)
        {
            // 0�ȏ�i�ȉ��̃����_���Ȑ������擾
            // Random.Range�̍ő�l�͑�Q���������Ȃ̂ŁA+1���邱�Ƃɒ���
            var j = Random.Range(0, i + 1);

            // i�Ԗڂ�j�Ԗڂ̗v�f����������
            var tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }
    }
}

public enum GameMode
{
    PlayerOnly
}