using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandSwitch : MonoBehaviour
{
    public bool LeftHandUse;
    public GameObject RacketSetPos, RacketScript;
    public Vector3 Rsetpos, Lsetpos;
    public Vector3 Rsetrot, Lsetrot;
    public AudioClip Change;
    public Text GuidText1, GuidText2, TestText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Right()
    {
        if (!LeftHandUse)
        {
            return;
        }
        GetComponent<AudioSource>().PlayOneShot(Change);
        LeftHandUse = false;
        RacketScript.GetComponent<RacketPos>().LeftHandUse = false;
        RacketSetPos.transform.localPosition = Rsetpos;
        RacketSetPos.transform.localEulerAngles = Rsetrot;
        GetComponent<Avatar>().HandSwitch();
        GuidText1.text = "X�{�^��";
        GuidText2.text = "A�{�^��";
        TestText.text = "Y�{�^��";
    }
    public void Left()
    {
        if (LeftHandUse)
        {
            return;
        }
        GetComponent<AudioSource>().PlayOneShot(Change);
        LeftHandUse = true;
        RacketScript.GetComponent<RacketPos>().LeftHandUse = true;
        RacketSetPos.transform.localPosition = Lsetpos;
        RacketSetPos.transform.localEulerAngles = Lsetrot;
        GetComponent<Avatar>().HandSwitch();
        GuidText1.text = "A�{�^��";
        GuidText2.text = "X�{�^��";
        TestText.text = "B�{�^��";
    }
}
