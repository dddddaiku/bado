using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;

public class shuttleInstance : MonobitEngine.MonoBehaviour
{

    public GameObject S, Racket, Manager, Lhand,Rhand, Pod;
    public Vector3 NowPos, OldPos;
    public float Desi, Num, ShotPower;
    public float NowVelo, Acc, OldVelo;
    public bool test, Receiver, LeftHandUser;
    public int val;

    //シャトルの可変パラメーター
    public float ShuttoleGram;
    //public float ShuttoleCor;
    public float ShuttoleDrag;
    public float Deltime;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Receiver)
        {
            return;
        }
        if (test)
        {
            if (!Manager.GetComponent<HandSwitch>().LeftHandUse)
            {
                if (OVRInput.GetDown(OVRInput.RawButton.Y) && Manager.GetComponent<MatchManager>().MatchState == 0)
                {
                    if (S == null)
                    {
                        TestShot();
                    }
                }
                if (Input.GetKeyDown(KeyCode.B) && Manager.GetComponent<MatchManager>().MatchState == 0)
                {
                    if (S == null)
                    {
                        TestShot();
                    }
                }
            }
            else
            {
                if (OVRInput.GetDown(OVRInput.RawButton.B) && Manager.GetComponent<MatchManager>().MatchState == 0)
                {
                    if (S == null)
                    {
                        TestShot();
                    }
                }
            }
            return;
        }
        if (!Manager.GetComponent<HandSwitch>().LeftHandUse)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.X))
            {
                Instance(Lhand);
                
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                Instance(Rhand);
                
            }
        }


    }


    public void Instance(GameObject UseHand)
    {
        if (MonobitNetwork.inRoom)
        {
            val++;
            S = MonobitNetwork.Instantiate("shuttle", transform.position, new Quaternion(0, 0, 0, 0), 0);
            
            string Oname = S.name;
            S.name = S.name + val;
            monobitView.RPC("Instances", MonobitTargets.OthersBuffered, Oname, val);
            S.GetComponent<Rigidbody>().drag = ShuttoleDrag;
            S.GetComponent<Rigidbody>().mass = ShuttoleGram;
            S.GetComponent<Rigidbody>().useGravity = false;
            S.GetComponent<SphereCollider>().enabled = false;
            Racket.GetComponent<HitSpot>().ShuttlGram = ShuttoleGram;
            S.transform.parent = UseHand.transform;
            S.transform.localPosition = new Vector3(0, 0, 0);
            S.GetComponent<ShuttleRotate>().Hold = true;
           

            
        }
        else
        {
            val++;

            S = (GameObject)Instantiate(Resources.Load("shuttle"), transform.position, new Quaternion(0, 0, 0, 0));
            S.name = S.name + val;
            S.GetComponent<Rigidbody>().drag = ShuttoleDrag;
            S.GetComponent<Rigidbody>().mass = ShuttoleGram;
            S.GetComponent<Rigidbody>().useGravity = false;
            S.GetComponent<SphereCollider>().enabled = false;
            Racket.GetComponent<HitSpot>().ShuttlGram = ShuttoleGram;
            S.transform.parent = UseHand.transform;
            S.transform.localPosition = new Vector3(0, 0, 0);
            S.GetComponent<ShuttleRotate>().Hold = true;
            
        }

    }
    [MunRPC]
    public void Instances(string name, int number) 
    {

        val = number;
        S = GameObject.Find(name);
        S.name = S.name + val;
        S.GetComponent<Rigidbody>().drag = ShuttoleDrag;
        S.GetComponent<Rigidbody>().mass = ShuttoleGram;
        S.GetComponent<Rigidbody>().useGravity = false;
        S.GetComponent<SphereCollider>().enabled = false;
        Racket.GetComponent<HitSpot>().ShuttlGram = ShuttoleGram;
        
        S.GetComponent<ShuttleRotate>().Hold = true;


    }

    public void TestShot()
    {
        S = (GameObject)Instantiate(Resources.Load("shuttle"), transform.position, new Quaternion(0, 0, 0, 0));
        S.name = "Test君";
        S.GetComponent<Rigidbody>().drag = ShuttoleDrag;
        S.GetComponent<Rigidbody>().mass = ShuttoleGram;
        Racket.GetComponent<HitSpot>().ShuttlGram = ShuttoleGram;
        
        S.GetComponent<Rigidbody>().AddForce(this.transform.up * ShotPower, ForceMode.VelocityChange);
        Destroy(S, Deltime);
    }
}
