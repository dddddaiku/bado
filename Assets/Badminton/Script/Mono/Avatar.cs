using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.SceneManagement;


public class Avatar : MonobitEngine.MonoBehaviour
{
    // �v���n�u�������u�{�C�X�`���b�g�p�I�u�W�F�N�g�v�̃f�[�^
    public GameObject OnlineManager;
    public GameObject chatObj = null;
    
    
    
    string ServerName = "VRandWin";
    string RoomName = "Solo";
    //[SerializeField, Tooltip("�Đڑ���UI��������")]
    //GameObject ReconnectUI;
    // Transform Parent;
    // [SerializeField, Tooltip("���������v���C���[�̎q�iVR�R���g���[���[�j�����܂�")]
    // Transform Child;

    // Start is called before the first frame update
    void Start()
    {

        OnlineManager = GameObject.Find("OnlineManager");
        RoomName = OnlineManager.GetComponent<RpbbyManager>().RoomName;
        // MUN�T�[�o�ɐڑ�����
        /*
        MonobitNetwork.autoJoinLobby = true;
        MonobitNetwork.ConnectServer(ServerName);
        if (MonobitNetwork.inLobby)
        {
            MonobitEngine.RoomSettings settings = new MonobitEngine.RoomSettings();
            settings.maxPlayers = 2;
            settings.isVisible = true;
            settings.isOpen = true;
            MonobitEngine.MonobitNetwork.JoinOrCreateRoom(RoomName, settings, null);

        }
        */
        Makeroom();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (OnlineManager != null && OnlineManager.GetComponent<RpbbyManager>().Online && !MonobitNetwork.inLobby)
        {
            MonobitNetwork.autoJoinLobby = true;
            MonobitNetwork.ConnectServer(ServerName);
        }

        // ���[�������ς݂Ń{�C�X�`���b�g�p�I�u�W�F�N�g���������ł���΁A�I�u�W�F�N�g�̓��I���������s����
        if (MonobitNetwork.inRoom && chatObj == null && !GetComponent<HandSwitch>().LeftHandUse)
        {
            chatObj = MonobitNetwork.Instantiate("playerDammy", Vector3.zero, Quaternion.identity, 0);
            Debug.Log("�E�����v���C���[�𐶐��I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");

            return;
        }
        if (MonobitNetwork.inRoom && chatObj == null && GetComponent<HandSwitch>().LeftHandUse)
        {
            chatObj = MonobitNetwork.Instantiate("playerDammyL", Vector3.zero, Quaternion.identity, 0);
            Debug.Log("�������v���C���[�𐶐��I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");

            return;
        }

    }
   
    
   
    // ���r�[�ɓ�����������̃R�[���o�b�N���\�b�h
    public void Makeroom()
    {
        // �����uVoiceChatTest�v�Ɠ����̃��[�������쐬�Ȃ�A���̃��[���쐬���ē�������
        // �����uVoiceChatTest�v�Ɠ����̃��[�����쐬�ς݂Ȃ�A���̃��[���ɓ�������
        if (MonobitNetwork.inLobby)
        {
            MonobitEngine.RoomSettings settings = new MonobitEngine.RoomSettings();
            settings.maxPlayers = 3;
            settings.isVisible = true;
            settings.isOpen = true;
            MonobitEngine.MonobitNetwork.JoinOrCreateRoom(RoomName, settings, null);
            
        }

    }
    
    

    public void Error()
    {//�ڑ��؂�̂Ƃ��ɌĂ�
        Destroy(chatObj);
        //ReconnectUI.SetActive(true);
    }
    public void HandSwitch()
    {//�����ĕύX���ɌĂ�
        if (MonobitNetwork.inRoom)
        {
            MonobitNetwork.Destroy(chatObj);
        }
        else
        {
            Destroy(chatObj);
        }


    }



    public void OnMonobitMaxConnectionReached()
    {
        Error();
    }


    public void OnMunCloudConnectionFailed(MonobitEngine.MunCloudConnectionFailedCause cause)
    {
        Error();
    }

    public void OnCreateRoomFailed(object[] codeAndMsg)
    {
        Error();
    }

    public void OnJoinRoomFailed(object[] codeAndMsg)
    {
        Error();
    }

    public void OnMonobitRandomJoinFailed(object[] codeAndMsg)
    {
        Error();
    }

    public void OnConnectionFail(MonobitEngine.DisconnectCause cause)
    {
        Error();
    }

    public void OnDisconnectedFromServer()
    {

        MonobitNetwork.LeaveRoom();
        Scene sampleScene1 = SceneManager.GetSceneByName("Room1");
        SceneManager.MoveGameObjectToScene(OnlineManager, sampleScene1);
        SceneManager.LoadScene("Robby");

        Error();
    }

    public void Reconnect()
    {
        // ReconnectUI.SetActive(false);
        MonobitNetwork.ConnectServer(ServerName);
    }

    public void GameOver()
    {
        Debug.Log("No��������");
    }
}
