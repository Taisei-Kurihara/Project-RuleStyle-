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
        //�v���C���[�̃f�[�^�̏�����
        //���΂ɐl�����ɍ��̂͂�낵���Ȃ��̂ŁB
        switch (_playernum)
        {
            case 2:
                playerData_One = new PlayerData();
                playerData_Two = new PlayerData();
                break;
            case 3:
                playerData_One = new PlayerData();
                playerData_Two = new PlayerData();
                playerData_Three= new PlayerData();
                break;
            case 4:
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
}

public enum GameMode
{
    PlayerOnly
}