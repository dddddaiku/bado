using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class ShuttleRotate : MonobitEngine.MonoBehaviour
{
    public bool Hited, OnFloor, Hold, LeftHandUser;
    public float Count, CoolDown, Vibration;
    private Rigidbody rb;
    [SerializeField] Vector3 Movedirection;
    // Start is called before the first frame update
    private Vector3 latestPos;  //前回のPosition
    public GameObject Manager;
    public AudioClip Shotse;
    private void Start()
    {

        Manager = GameObject.Find("Manager");
    }
    private void Update()
    {
        Movedirection = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
        latestPos = transform.position;  //前回のPositionの更新

        //ベクトルの大きさが0.01以上の時に向きを変える処理をする
        if (Movedirection.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(Movedirection); //向きを変更する
        }
        if (Hited)
        {
            HitedcoolDown();
        }
        if (!Manager.GetComponent<HandSwitch>().LeftHandUse)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.X) || OVRInput.GetDown(OVRInput.RawButton.Y)|| Input.GetKeyDown(KeyCode.B))
            {
                if (OnFloor && !Hold)
                {
                    monobitView.RPC("Destroies", MonobitTargets.OthersBuffered);
                    Destroy(this.gameObject, 0);
                }
            }
            if (OVRInput.GetUp(OVRInput.RawButton.X))
            {
                if (MonobitNetwork.inRoom)
                {
                    if (Hold && monobitView.isMine)
                    {
                        Release();
                    }
                }
                else
                {
                    if (Hold)
                    {
                        Release();
                    }
                }
                    
                    
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.RawButton.A) || OVRInput.GetDown(OVRInput.RawButton.B)|| Input.GetKeyDown(KeyCode.B))
            {
                if (OnFloor && !Hold)
                {
                    monobitView.RPC("Destroies", MonobitTargets.OthersBuffered);
                    Destroy(this.gameObject, 0);
                }
            }
            if (OVRInput.GetUp(OVRInput.RawButton.A))
            {
                if (MonobitNetwork.inRoom)
                {
                    if (Hold && monobitView.isMine)
                    {
                        Release();
                    }
                }
                else
                {
                    if (Hold)
                    {
                        Release();
                    }
                }
            }
        }

        if (Hold && MonobitNetwork.inRoom && monobitView.isMine)
        {
            Vector3 Pos = transform.position;
            Quaternion Rot = transform.rotation;
            monobitView.RPC("HoldShuttle", MonobitTargets.OthersBuffered, Pos, Rot);
        }
    }
    [MunRPC]
    public void HoldShuttle(Vector3 Spos, Quaternion Srot)
    {

        transform.position = Spos;
        transform.rotation = Srot;


    }
    public void Release()
    {
        Debug.Log("1");
        Hold = false;
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<SphereCollider>().enabled = true;
        if (MonobitNetwork.inRoom)
        {
            monobitView.RPC("Releases", MonobitTargets.OthersBuffered);
        }
    }
    [MunRPC]
    public void Releases()
    {
        Hold = false;
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<SphereCollider>().enabled = true;
    }
    [MunRPC]
    public void Destroies()
    {
        Destroy(this.gameObject, 0);
    }
    public void HitedcoolDown()
    {
        Count++;
        if (Count > Vibration && Count < CoolDown)
        {

            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            GetComponent<SphereCollider>().enabled = true;
        }
        if (Count > CoolDown)
        {

            Hited = false;
            Count = 0;

        }
    }
    public void Fly(Vector3 ShotSpeed)
    {

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<SphereCollider>().enabled = false;
        transform.parent = null;
        Hited = true;
        GetComponent<Rigidbody>().AddForce(ShotSpeed, ForceMode.VelocityChange);

        if (MonobitNetwork.inRoom)
        {
            monobitView.RPC("Flies", MonobitTargets.OthersBuffered, ShotSpeed, transform.position, transform.rotation);
        }
    }
    [MunRPC]
    public void Flies(Vector3 ShotSpeed, Vector3 Pos, Quaternion Rot)
    {
        transform.position = Pos;
        transform.rotation = Rot;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<SphereCollider>().enabled = false;
        transform.parent = null;
        Hited = true;
        GetComponent<Rigidbody>().AddForce(ShotSpeed, ForceMode.VelocityChange);
        GetComponent<AudioSource>().PlayOneShot(Shotse);
    }
    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Floor") && !OnFloor)
        {
            OnFloor = Manager.GetComponent<MatchManager>().Score(10);
        }
        if (collision.gameObject.CompareTag("Coat1") && !OnFloor)
        {

            OnFloor = Manager.GetComponent<MatchManager>().Score(1);
        }
        if (collision.gameObject.CompareTag("Coat2") && !OnFloor)
        {
            OnFloor = Manager.GetComponent<MatchManager>().Score(2);
        }

    }
}

