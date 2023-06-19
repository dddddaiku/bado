using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;
using UnityEngine.SceneManagement;

public class RpbbyManager : MonobitEngine.MonoBehaviour
{
    public string RoomName;
    public string ServerName = "VRandWin";
    public bool Online,Next;
    public GameObject Window,MultiButton;
    public GameObject[] Buttons;
    public Text[] RoomMens;
    public RoomData[] info = new RoomData[5];
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        //Unity.XR.Oculus.Performance.TrySetDisplayRefreshRate(90);
        //OVRPlugin.systemDisplayFrequency = 90f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Next)
        {
            return;
        }
        if (!MonobitNetwork.inLobby)
        {
            MultiButton.GetComponent<Button>().interactable = false;
            Window.SetActive(false);
            MonobitNetwork.autoJoinLobby = true;
            MonobitNetwork.ConnectServer(ServerName);
        }
        else
        {
            MultiButton.GetComponent<Button>().interactable = true;
        }
        if (!Window)
        {
            return;
        }
        if (Window.activeSelf)
        {

            RoomCheck();
        }
    }
    public void RoomList()
    {
        Window.SetActive(true);
    }
    public void Back()
    {
        Window.SetActive(false);
    }
    public void Solo()
    {
        Online = false;
        Next = true;
        RoomName = "Solo";
        SceneManager.LoadScene("Room1");
    }
    public void OnlineRoomMake(string Roomname)
    {
        Online = true;
        Next = true;
        RoomName = Roomname;
        SceneManager.LoadScene("Room1");
    }
    public void RoomCheck()
    {
        info = MonobitEngine.MonobitNetwork.GetRoomData();
        Debug.Log(info.Length);
        for (int i = 0; i < 5; i++)
        {
            RoomMens[i].text = "人数：0/2";
            Buttons[i].GetComponent<Button>().interactable = true;
        }
            for (int i = 0; i < info.Length; i++)
        {

            Debug.Log(info[i].name);
            if (info[i].name == "RoomA")
            {
                RoomMens[0].text = "人数：" + info[i].playerCount + "/2";
                if (info[i].playerCount >= info[i].maxPlayers - 1)
                {
                    Buttons[0].GetComponent<Button>().interactable = false;
                }
                else
                {
                    Buttons[0].GetComponent<Button>().interactable = true;
                }
            }
            if (info[i].name == "RoomB")
            {
                RoomMens[1].text = "人数：" + info[i].playerCount + "/2";
                if (info[i].playerCount >= info[i].maxPlayers - 1)
                {
                    Buttons[1].GetComponent<Button>().interactable = false;
                }
                else
                {
                    Buttons[1].GetComponent<Button>().interactable = true;
                }
            }
            if (info[i].name == "RoomC")
            {
                RoomMens[2].text = "人数：" + info[i].playerCount + "/2";
                if (info[i].playerCount >= info[i].maxPlayers - 1)
                {
                    Buttons[2].GetComponent<Button>().interactable = false;
                }
                else
                {
                    Buttons[2].GetComponent<Button>().interactable = true;
                }
            }
            if (info[i].name == "RoomD")
            {
                RoomMens[3].text = "人数：" + info[i].playerCount + "/2";
                if (info[i].playerCount >= info[i].maxPlayers - 1)
                {
                    Buttons[3].GetComponent<Button>().interactable = false;
                }
                else
                {
                    Buttons[3].GetComponent<Button>().interactable = true;
                }
            }
            if (info[i].name == "RoomE")
            {
                RoomMens[4].text = "人数：" + info[i].playerCount + "/2";
                if (info[i].playerCount >= info[i].maxPlayers - 1)
                {
                    Buttons[4].GetComponent<Button>().interactable = false;
                }
                else
                {
                    Buttons[4].GetComponent<Button>().interactable = true;
                }
            }



        }
    }
}
