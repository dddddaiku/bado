using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.UI;



public class EnviromentSet : MonobitEngine.MonoBehaviour
{
    public GameObject Manager, Inst, Gun;
    public GameObject Japan, Desert, Snow;
    public GameObject[] Slec;
    public float DragVal, GravityVal, RacketCoRVal;
    public float BaseDragVal, BaseGravityVal, BaseRacketCoRVal;
    public AudioClip Sel;
    public Text NowState;
    // Start is called before the first frame update
    void Start()
    {
        Inst.GetComponent<shuttleInstance>().ShuttoleDrag = DragVal;
        Gun.GetComponent<shuttleInstance>().ShuttoleDrag = DragVal;
    }



    // Update is called once per frame
    void Update()
    {
        Inst.GetComponent<shuttleInstance>().ShuttoleDrag = DragVal;
        Gun.GetComponent<shuttleInstance>().ShuttoleDrag = DragVal;
        Physics.gravity = new Vector3(0, GravityVal, 0);
        Manager.GetComponent<PhysicsCulc>().Rcor = RacketCoRVal;
    }
    public void Env1()
    {
        DragVal = BaseDragVal;
        RacketCoRVal = BaseRacketCoRVal;
        NowState.text = "ê›íËÅFä¬ã´á@";
        Manager.GetComponent<AudioSource>().PlayOneShot(Sel);
        for (int i = 0; i < Slec.Length; i++)
        {
            Slec[i].SetActive(false);
        }
        Slec[0].SetActive(true);
        if (MonobitNetwork.inRoom)
        {
            monobitView.RPC("Change", MonobitTargets.OthersBuffered, DragVal, RacketCoRVal, "ê›íËÅFä¬ã´á@", 0);
        }
    }
    public void Env2()
    {
        DragVal = BaseDragVal * 1.5f;
        RacketCoRVal = BaseRacketCoRVal * 1.5f;
        NowState.text = "ê›íËÅFä¬ã´áA";
        Manager.GetComponent<AudioSource>().PlayOneShot(Sel);
        for (int i = 0; i < Slec.Length; i++)
        {
            Slec[i].SetActive(false);
        }
        Slec[1].SetActive(true);
        if (MonobitNetwork.inRoom)
        {
            monobitView.RPC("Change", MonobitTargets.OthersBuffered, DragVal, RacketCoRVal, "ê›íËÅFä¬ã´áA", 1);
        }
    }
    public void Env3()
    {
        DragVal = BaseDragVal * 1.5f;
        RacketCoRVal = BaseRacketCoRVal;
        NowState.text = "ê›íËÅFä¬ã´áB";
        Manager.GetComponent<AudioSource>().PlayOneShot(Sel);
        for (int i = 0; i < Slec.Length; i++)
        {
            Slec[i].SetActive(false);
        }
        Slec[2].SetActive(true);
        if (MonobitNetwork.inRoom)
        {
            monobitView.RPC("Change", MonobitTargets.OthersBuffered, DragVal, RacketCoRVal, "ê›íËÅFä¬ã´áB", 2);
        }
    }
    public void Japane()
    {
        DragVal = BaseDragVal;
        RacketCoRVal = BaseRacketCoRVal;
        NowState.text = "ê›íËÅFì˙ñ{";
        Manager.GetComponent<AudioSource>().PlayOneShot(Sel);
        Desert.SetActive(false);
        Snow.SetActive(false);
        Japan.SetActive(true);
        for (int i = 0; i < Slec.Length; i++)
        {
            Slec[i].SetActive(false);
        }
        Slec[3].SetActive(true);
        if (MonobitNetwork.inRoom)
        {
            monobitView.RPC("Change", MonobitTargets.OthersBuffered, DragVal, RacketCoRVal, "ê›íËÅFì˙ñ{", 3);
        }
    }
    public void Des()
    {
        DragVal = BaseDragVal * 0.8f;
        RacketCoRVal = BaseRacketCoRVal;
        NowState.text = "ê›íËÅFçªîô";
        Manager.GetComponent<AudioSource>().PlayOneShot(Sel);
        Snow.SetActive(false);
        Japan.SetActive(false);
        Desert.SetActive(true);
        for (int i = 0; i < Slec.Length; i++)
        {
            Slec[i].SetActive(false);
        }
        Slec[4].SetActive(true);
        if (MonobitNetwork.inRoom)
        {
            monobitView.RPC("Change", MonobitTargets.OthersBuffered, DragVal, RacketCoRVal, "ê›íËÅFçªîô", 4);
        }
    }
    public void Noth()
    {
        DragVal = BaseDragVal * 1.2f;
        RacketCoRVal = BaseRacketCoRVal;
        NowState.text = "ê›íËÅFñkã…";
        Manager.GetComponent<AudioSource>().PlayOneShot(Sel);
        Japan.SetActive(false);
        Desert.SetActive(false);
        Snow.SetActive(true);
        for (int i = 0; i < Slec.Length; i++)
        {
            Slec[i].SetActive(false);
        }
        Slec[5].SetActive(true);
        if (MonobitNetwork.inRoom)
        {
            monobitView.RPC("Change", MonobitTargets.OthersBuffered, DragVal, RacketCoRVal, "ê›íËÅFñkã…", 5);
        }
    }
    [MunRPC]
    public void Change(float Drag, float Cor, string env, int select)
    {
        DragVal = Drag;
        RacketCoRVal = Cor;
        NowState.text = "" + env;
        Manager.GetComponent<AudioSource>().PlayOneShot(Sel);
        for (int i = 0; i < Slec.Length; i++)
        {
            Slec[i].SetActive(false);
        }
        Slec[select].SetActive(true);
        switch (env)
        {
            case "ê›íËÅFì˙ñ{":
                Desert.SetActive(false);
                Snow.SetActive(false);
                Japan.SetActive(true);
                break;
            case "ê›íËÅFçªîô":
                Snow.SetActive(false);
                Japan.SetActive(false);
                Desert.SetActive(true);
                break;
            case "ê›íËÅFñkã…":
                Japan.SetActive(false);
                Desert.SetActive(false);
                Snow.SetActive(true);
                break;
        }

    }
}