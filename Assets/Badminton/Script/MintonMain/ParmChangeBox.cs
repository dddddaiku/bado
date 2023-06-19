using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;

public class ParmChangeBox : MonobitEngine.MonoBehaviour
{
    public GameObject Manager,Inst,Gun;
    public float BaseDragVal, BaseGravityVal, BaseRacketCoRVal;
    public float DragVal, GravityVal,RacketCoRVal;
    public Text[] MagText;
    public float[] Setmagnification;
    public int NeutralMagNum,GNum, DNum, CNum;
    public AudioClip Upsound, Downsound;
    // Start is called before the first frame update
    void Start()
    {
        DragVal = BaseDragVal * Setmagnification[NeutralMagNum];
        GravityVal = BaseGravityVal * Setmagnification[NeutralMagNum];
        RacketCoRVal = BaseRacketCoRVal * Setmagnification[NeutralMagNum];
        Manager.GetComponent<PhysicsCulc>().Rcor = RacketCoRVal;
        GNum = NeutralMagNum;DNum = NeutralMagNum; CNum = NeutralMagNum;
        Physics.gravity = new Vector3(0, GravityVal, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Inst.GetComponent<shuttleInstance>().ShuttoleDrag = DragVal;
        Gun.GetComponent<shuttleInstance>().ShuttoleDrag = DragVal;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("bole"))
        {
            
                other.GetComponent<Rigidbody>().drag = DragVal;
                Physics.gravity = new Vector3(0, GravityVal, 0);
                 Manager.GetComponent<PhysicsCulc>().Rcor = RacketCoRVal;
            Inst.GetComponent<shuttleInstance>().ShuttoleDrag = DragVal;

        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        /*
        if (other.gameObject.CompareTag("bole"))
        {
            //åªé¿ÇÃíl
            other.GetComponent<Rigidbody>().drag = BaseDragVal;
                
            Physics.gravity = new Vector3(0,BaseGravityVal, 0);
            Manager.GetComponent<PhysicsCulc>().Rcor = BaseRacketCoRVal;
        }
        */
    }
    public void ChangeGravity(bool up)
    {
        if (up && GNum < Setmagnification.Length -1)
        {
            GNum++;
            GravityVal = GravityVal = BaseGravityVal * Setmagnification[GNum];
            MagText[0].text = "Å~"+ Setmagnification[GNum];
            monobitView.RPC("ChangeGravities", MonobitTargets.OthersBuffered, GNum);
            GetComponent<AudioSource>().PlayOneShot(Upsound);
        }
        if (!up && GNum > 0)
        {
            GNum--;
            GravityVal = GravityVal = BaseGravityVal * Setmagnification[GNum];
            MagText[0].text = "Å~" + Setmagnification[GNum];
            monobitView.RPC("ChangeGravities", MonobitTargets.OthersBuffered, GNum);
            GetComponent<AudioSource>().PlayOneShot(Downsound);
        }
    }
    [MunRPC]
    public void ChangeGravities(int Val)
    {
        GNum = Val;
        GravityVal = BaseGravityVal * Setmagnification[GNum];
        MagText[0].text = "Å~" + Setmagnification[GNum];
    }
    public void ChangeDrag(bool up)
    {
        if (up && DNum < Setmagnification.Length -1)
        {
            DNum++;
            DragVal = BaseDragVal * Setmagnification[DNum];
            MagText[1].text = "Å~" + Setmagnification[DNum];
            monobitView.RPC("ChangeDrags", MonobitTargets.OthersBuffered, DNum);
            GetComponent<AudioSource>().PlayOneShot(Upsound);
        }
        if (!up && DNum > 0)
        {
            DNum--;
            DragVal = BaseDragVal * Setmagnification[DNum];
            MagText[1].text = "Å~" + Setmagnification[DNum];
            monobitView.RPC("ChangeDrags", MonobitTargets.OthersBuffered, DNum);
            GetComponent<AudioSource>().PlayOneShot(Downsound);
        }
    }
    [MunRPC]
    public void ChangeDrags(int Val)
    {
        DNum = Val;
        DragVal = BaseDragVal * Setmagnification[DNum];
        MagText[1].text = "Å~" + Setmagnification[DNum];
    }
    public void ChangeCoR(bool up)
    {
        if (up && CNum < Setmagnification.Length - 1)
        {
            CNum++;
            RacketCoRVal = BaseRacketCoRVal * Setmagnification[CNum];
            MagText[2].text = "Å~" + Setmagnification[CNum];
            monobitView.RPC("ChangeCoRs", MonobitTargets.OthersBuffered, CNum);
            GetComponent<AudioSource>().PlayOneShot(Upsound);
        }
        if (!up && CNum > 0)
        {
            CNum--;
            RacketCoRVal = BaseRacketCoRVal * Setmagnification[CNum];
            MagText[2].text = "Å~" + Setmagnification[CNum];
            monobitView.RPC("ChangeCoRs", MonobitTargets.OthersBuffered, CNum);
            GetComponent<AudioSource>().PlayOneShot(Downsound);
        }
    }
    [MunRPC]
    public void ChangeCoRs(int Val)
    {
        CNum = Val;
        RacketCoRVal = BaseRacketCoRVal * Setmagnification[CNum];
        MagText[2].text = "Å~" + Setmagnification[CNum];
    }
}
