using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.UI;

public class MatchManager : MonobitEngine.MonoBehaviour
{
    public int MyCoat, RivalCoat;//0����1�I�����W2�� My�͎������������Ƃ��ARival��Monovito���L
    public int LastShooter, MatchState, ServeCoat;//0���I�����W�R�[�g�A1���R�[�g
    public GameObject Instance;
    public bool WaitMy, WaitRival;
    public Text[] PreText, PointTex, AnnouncementTex;
    public int[] GetPoint = new int[2];
    public int Maxpoint;
    public AudioClip Ok, No, StartH, EndH, ShortH;
    // Start is called before the first frame update
    void Start()
    {
        WaitMy = true;
        WaitRival = true;
        Instance.GetComponent<shuttleInstance>().Receiver = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MatchButton(int side)
    {
        if (MatchState != 0)
        {
            return;
        }
        if (MyCoat == side)
        {
            GetComponent<AudioSource>().PlayOneShot(Ok);
            MyCoat = 0;
            Debug.Log("����ς���Ȃ�");
            WaitMy = true;
            PreText[side - 1].text = "�v���C���[�҂��c";
            StartCoroutine(Wait(0.2f));
            monobitView.RPC("MatchButtons", MonobitTargets.OthersBuffered, MyCoat);
        }
        else if (side != 0 && WaitMy && RivalCoat != side)
        {
            GetComponent<AudioSource>().PlayOneShot(Ok);
            MyCoat = side;
            WaitMy = false;
            PreText[side - 1].text = "���Ȃ����Q��������";

            if (!WaitMy && !WaitRival)
            {
                monobitView.RPC("MatchButtons", MonobitTargets.OthersBuffered, MyCoat);
                MatchStart();
            }
            else
            {
                monobitView.RPC("MatchButtons", MonobitTargets.OthersBuffered, MyCoat);
            }

            StartCoroutine(Wait(0.2f));
        }
        else if (RivalCoat == side)
        {
            GetComponent<AudioSource>().PlayOneShot(No);

            PreText[side - 1].text = "���肪�Q��������";
            StartCoroutine(Wait(0.2f));
        }
    }
    [MunRPC]
    public void MatchButtons(int rivalcoat)
    {
        if (rivalcoat == 0)
        {
            GetComponent<AudioSource>().PlayOneShot(Ok);
            PreText[RivalCoat - 1].text = "�v���C���[�҂��c";
            WaitRival = true;
            RivalCoat = 0;
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(Ok);
            PreText[rivalcoat - 1].text = "���肪�Q��������";
            WaitRival = false;
        }
        RivalCoat = rivalcoat;

    }

    public void MatchStart()
    {
        Debug.Log("���ҏ��������I");
        MatchState = 1;
        ServeCoat = Random.Range(1, 3);
        Debug.Log(ServeCoat);
        GetComponent<AudioSource>().PlayOneShot(StartH);
        if (ServeCoat == MyCoat)
        {
            Instance.GetComponent<shuttleInstance>().Receiver = false;
        }
        else
        {
            Instance.GetComponent<shuttleInstance>().Receiver = true;
        }
        for (int i = 0; i < PointTex.Length; i++)
        {
            PointTex[i].text = "0";
            GetPoint[i] = 0;
            if (i + 1 == ServeCoat)
            {
                AnnouncementTex[i].text = "���Ȃ��̃T�[�u�ł�";
                LastShooter = ServeCoat;

            }
            else
            {
                AnnouncementTex[i].text = "�����ẴT�[�u�ł�";
                LastShooter = ServeCoat;

            }
        }
        for (int i = 0; i < PointTex.Length; i++)
        {
            PreText[i].text = "������";
        }

        monobitView.RPC("MatchStarts", MonobitTargets.OthersBuffered, ServeCoat);
        StartCoroutine(Wait(1f));
    }
    [MunRPC]
    public void MatchStarts(int serveCoat)
    {
        MatchState = 1;
        ServeCoat = serveCoat;
        GetComponent<AudioSource>().PlayOneShot(StartH);
        if (ServeCoat == MyCoat)
        {
            Instance.GetComponent<shuttleInstance>().Receiver = false;
        }
        else
        {
            Instance.GetComponent<shuttleInstance>().Receiver = true;
        }
        for (int i = 0; i < PointTex.Length; i++)
        {
            PointTex[i].text = "0";
            GetPoint[i] = 0;
            if (i + 1 == ServeCoat)
            {
                AnnouncementTex[i].text = "���Ȃ��̃T�[�u�ł�";
                LastShooter = ServeCoat;

            }
            else
            {
                AnnouncementTex[i].text = "�����ẴT�[�u�ł�";
                LastShooter = ServeCoat;

            }
        }
        for (int i = 0; i < PointTex.Length; i++)
        {
            PreText[i].text = "������";
        }
        StartCoroutine(Wait(1f));
    }
    IEnumerator Wait(float Second)
    {
        OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.RTouch);
        yield return new WaitForSeconds(Second);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
    public bool Score(int Oncoat)
    {
        if (MatchState == 0)
        {
            return true;
        }
        if (LastShooter == MyCoat)
        {
            return false;
        }
        GetComponent<AudioSource>().PlayOneShot(ShortH);
        if (Oncoat == MyCoat)
        {
            GetPoint[RivalCoat - 1] += 1;
            ServeCoat = RivalCoat;
            Instance.GetComponent<shuttleInstance>().Receiver = true;
            for (int i = 0; i < PointTex.Length; i++)
            {
                PointTex[i].text = "" + GetPoint[i];
                if (i + 1 == ServeCoat)
                {
                    AnnouncementTex[i].text = "���Ȃ��̃T�[�u�ł�";
                    LastShooter = ServeCoat;

                }
                else
                {
                    AnnouncementTex[i].text = "�����ẴT�[�u�ł�";
                    LastShooter = ServeCoat;

                }
            }
            StartCoroutine(Wait(1f));
        }
        else
        {
            GetPoint[MyCoat - 1] += 1;
            ServeCoat = MyCoat;
            Instance.GetComponent<shuttleInstance>().Receiver = false;
            for (int i = 0; i < PointTex.Length; i++)
            {
                PointTex[i].text = "" + GetPoint[i];
                if (i + 1 == ServeCoat)
                {
                    AnnouncementTex[i].text = "���Ȃ��̃T�[�u�ł�";
                    LastShooter = ServeCoat;

                }
                else
                {
                    AnnouncementTex[i].text = "�����ẴT�[�u�ł�";
                    LastShooter = ServeCoat;

                }
            }
            StartCoroutine(Wait(1f));
        }
        if (GetPoint[0] == Maxpoint || GetPoint[1] == Maxpoint)
        {

            if (GetPoint[0] == Maxpoint)
            {
                AnnouncementTex[0].text = "���Ȃ��̏����ł�";
                AnnouncementTex[1].text = "���Ȃ��̕����ł�";
            }
            else
            {
                AnnouncementTex[1].text = "���Ȃ��̏����ł�";
                AnnouncementTex[0].text = "���Ȃ��̕����ł�";
            }
            GetComponent<AudioSource>().PlayOneShot(EndH);
            MatchState = 0;
            Instance.GetComponent<shuttleInstance>().Receiver = false;
            WaitMy = true;
            WaitRival = true;
            MyCoat = 0;
            RivalCoat = 0;
            for (int i = 0; i < PointTex.Length; i++)
            {
                PreText[i].text = "�v���C���[�҂��c";
            }
        }
        monobitView.RPC("Scores", MonobitTargets.OthersBuffered, Oncoat);
        return true;

    }
    public void Shot()
    {
        LastShooter = MyCoat;
        monobitView.RPC("Shots", MonobitTargets.OthersBuffered, MyCoat);
    }
    [MunRPC]
    public void Shots(int coat)
    {
        LastShooter = coat;
    }
    [MunRPC]
    public void Scores(int Oncoat)
    {
        GetComponent<AudioSource>().PlayOneShot(ShortH);

        if (Oncoat == RivalCoat)
        {
            GetPoint[MyCoat - 1] += 1;
            ServeCoat = MyCoat;

            Instance.GetComponent<shuttleInstance>().Receiver = false;
            for (int i = 0; i < PointTex.Length; i++)
            {
                PointTex[i].text = "" + GetPoint[i];
                if (i + 1 == ServeCoat)
                {
                    AnnouncementTex[i].text = "���Ȃ��̃T�[�u�ł�";
                    LastShooter = ServeCoat;

                }
                else
                {
                    AnnouncementTex[i].text = "�����ẴT�[�u�ł�";
                    LastShooter = ServeCoat;

                }
            }
            StartCoroutine(Wait(1f));
        }
        else
        {
            GetPoint[RivalCoat - 1] += 1;
            ServeCoat = RivalCoat;
            Instance.GetComponent<shuttleInstance>().Receiver = true;
            for (int i = 0; i < PointTex.Length; i++)
            {
                PointTex[i].text = "" + GetPoint[i];
                if (i + 1 == ServeCoat)
                {
                    AnnouncementTex[i].text = "���Ȃ��̃T�[�u�ł�";
                    LastShooter = ServeCoat;

                }
                else
                {
                    AnnouncementTex[i].text = "�����ẴT�[�u�ł�";
                    LastShooter = ServeCoat;

                }
            }
            StartCoroutine(Wait(1f));
        }
        if (Instance.GetComponent<shuttleInstance>().S != null)
        {
            Instance.GetComponent<shuttleInstance>().S.GetComponent<ShuttleRotate>().OnFloor = true;
        }
        if (GetPoint[0] == Maxpoint || GetPoint[1] == Maxpoint)
        {

            if (GetPoint[0] == Maxpoint)
            {
                AnnouncementTex[0].text = "���Ȃ��̏����ł�";
                AnnouncementTex[1].text = "���Ȃ��̕����ł�";
            }
            else
            {
                AnnouncementTex[1].text = "���Ȃ��̏����ł�";
                AnnouncementTex[0].text = "���Ȃ��̕����ł�";
            }
            GetComponent<AudioSource>().PlayOneShot(EndH);
            MatchState = 0;
            Instance.GetComponent<shuttleInstance>().Receiver = false;
            WaitMy = true;
            WaitRival = true;
            MyCoat = 0;
            RivalCoat = 0;
            for (int i = 0; i < PointTex.Length; i++)
            {
                PreText[i].text = "�v���C���[�҂��c";
            }
        }

    }
}

