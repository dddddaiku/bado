using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.SceneManagement;


public class Avatar : MonobitEngine.MonoBehaviour
{
    // プレハブ化した「ボイスチャット用オブジェクト」のデータ
    public GameObject OnlineManager;
    public GameObject chatObj = null;
    
    
    
    string ServerName = "VRandWin";
    string RoomName = "Solo";
    //[SerializeField, Tooltip("再接続のUIを代入する")]
    //GameObject ReconnectUI;
    // Transform Parent;
    // [SerializeField, Tooltip("生成されるプレイヤーの子（VRコントローラー）を入れます")]
    // Transform Child;

    // Start is called before the first frame update
    void Start()
    {

        OnlineManager = GameObject.Find("OnlineManager");
        RoomName = OnlineManager.GetComponent<RpbbyManager>().RoomName;
        // MUNサーバに接続する
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

        // ルーム入室済みでボイスチャット用オブジェクトが未生成であれば、オブジェクトの動的生成を実行する
        if (MonobitNetwork.inRoom && chatObj == null && !GetComponent<HandSwitch>().LeftHandUse)
        {
            chatObj = MonobitNetwork.Instantiate("playerDammy", Vector3.zero, Quaternion.identity, 0);
            Debug.Log("右利きプレイヤーを生成！！！！！！！！！！！！！！！");

            return;
        }
        if (MonobitNetwork.inRoom && chatObj == null && GetComponent<HandSwitch>().LeftHandUse)
        {
            chatObj = MonobitNetwork.Instantiate("playerDammyL", Vector3.zero, Quaternion.identity, 0);
            Debug.Log("左利きプレイヤーを生成！！！！！！！！！！！！！！！");

            return;
        }

    }
   
    
   
    // ロビーに入室した直後のコールバックメソッド
    public void Makeroom()
    {
        // もし「VoiceChatTest」と同名のルームが未作成なら、そのルーム作成して入室する
        // もし「VoiceChatTest」と同名のルームが作成済みなら、そのルームに入室する
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
    {//接続切れのときに呼ぶ
        Destroy(chatObj);
        //ReconnectUI.SetActive(true);
    }
    public void HandSwitch()
    {//持ちて変更時に呼ぶ
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
        Debug.Log("Noを押した");
    }
}
