using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;

public class PhysicsCulc : MonobitEngine.MonoBehaviour
{
    
    //計算使用パラメーター
    public float Rgram, Rcor;
    public Vector3 Rspeed;
    public float Sgram, Scor, SDrag;
    public Vector3 Sspeed;
    public float Magnification, RefMagnification, Numshot;
    public float[] Num = new float[5];
    public Text Nowval;
    //出力パワー
    public Vector3 ShotSpeed;
    public AudioClip Shot;
    public float MaxRS, Maxspeed, Minspeed;
    public bool Testmode;
    public float TestRS;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 Culc(Vector3 vec, Vector3 Sspeed, float Sgram)
    {
        float RS;
        if (Testmode)
        {
             RS = TestRS;
        }
        else
        {
            RS = Rspeed.magnitude;
        }
       
        Debug.Log(RS);
        if (RS < 0.3f)
        {
            Numshot = 1;
            RefMagnification = 0.8f;
            Nowval.text = "0";
        }
        else if (RS > MaxRS)
        {
            Numshot = Maxspeed;
            RefMagnification = 0.2f;
            Nowval.text = "" + Numshot;
        }
        else
        {
            Numshot = ((((float)RS / (float)MaxRS) * (Maxspeed - Minspeed)) + Minspeed);
            RefMagnification = 1;
            Nowval.text = "" + Numshot;
        }
        GetComponent<AudioSource>().PlayOneShot(Shot);
       return ShotSpeed = (((((vec.normalized + Rspeed.normalized).normalized * Numshot + Sspeed.normalized * -1 * RefMagnification) * Rgram * Magnification)) * Rcor) / Sgram;
       //return ShotSpeed = (((((vec.normalized + Rspeed.normalized).normalized * Maxspeed + Sspeed.normalized * -1 * RefMagnification) * Rgram * Magnification)) * Rcor) / Sgram;

    }

}
