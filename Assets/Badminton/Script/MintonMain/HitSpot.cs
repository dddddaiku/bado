using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;


public class HitSpot : MonobitEngine.MonoBehaviour
{
    private Rigidbody rb;
    public float Count;
    public Vector3 NowRacketPos, OldRacketPos, Racketacceleration, RacketSpeed;
    public Vector3 RacketMotion, RacketFace;
    public Vector3 ShuttlePos;
    public Vector3 ShotPower, Vec, Sspeed;
    public float ShuttlGram;
    public GameObject Manager;
    public bool Surface;//0Left,1Right
    //ラケット可変パラメーター
    public float RacketGram, RacketCor;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        RacketParm();
        SendParm();
        ColiderSwitch();
        Vec = transform.localRotation * transform.forward;
    }
    public void RacketParm()
    {
        NowRacketPos = transform.position;
        RacketSpeed = (NowRacketPos - OldRacketPos) / Time.deltaTime;

        Racketacceleration = RacketSpeed / Time.deltaTime;
        RacketMotion = rb.velocity.normalized;
        OldRacketPos = NowRacketPos;
        // RacTex[0].text = "" + Mathf.Abs(Racketacceleration.magnitude);
        // RacTex[1].text = "" + Mathf.Abs(RacketSpeed.magnitude);

    }
    public void SendParm()
    {
        Manager.GetComponent<PhysicsCulc>().Rspeed = RacketSpeed;
        Manager.GetComponent<PhysicsCulc>().Rgram = RacketGram;
        //Manager.GetComponent<PhysicsCulc>().Rcor = RacketCor;
    }
    public void ColiderSwitch()
    {
        
        if ((RacketSpeed.normalized - Vec.normalized).magnitude > (RacketSpeed.normalized - -1 * Vec.normalized).magnitude)
        {
            Surface = false;
        }
        else
        {
            Surface = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("bole"))
        {
            if (!collision.gameObject.GetComponent<ShuttleRotate>().Hited)
            {
                if (!Manager.GetComponent<HandSwitch>().LeftHandUse)
                {
                    OVRInput.SetControllerVibration(1, 1f, OVRInput.Controller.RTouch);
                }
                else
                {
                    OVRInput.SetControllerVibration(1, 1f, OVRInput.Controller.LTouch);
                }
                
                collision.gameObject.GetComponent<ShuttleRotate>().Hited = true;
                if (Surface)
                {
                    Vec = transform.localRotation * transform.forward;
                }
                else
                {
                    Vec = transform.localRotation * transform.forward * -1;
                }
                ShuttlGram = collision.gameObject.GetComponent<Rigidbody>().mass;
                Sspeed = collision.gameObject.GetComponent<Rigidbody>().velocity;
                collision.gameObject.GetComponent<ShuttleRotate>().Fly(Manager.GetComponent<PhysicsCulc>().Culc(Vec, Sspeed, ShuttlGram));
                Manager.GetComponent<MatchManager>().Shot();
                ShuttlePos = collision.gameObject.transform.position;

            }



        }
    }




}
