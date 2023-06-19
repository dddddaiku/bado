using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCtrl : MonoBehaviour
{
    public GameObject ParmGraf,ShuttleParam, RacketParam,UIhelper;
    public bool On;
    // Start is called before the first frame update
    void Start()
    {
        UIhelper.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<HandSwitch>().LeftHandUse)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.A) && On)
            {
                On = false;
                UIhelper.SetActive(false);
                RacketParam.transform.localPosition = new Vector3(99, 99, 99);

            }
            else if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                On = true;
                UIhelper.SetActive(true);
                RacketParam.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.RawButton.X) && On)
            {
                On = false;
                UIhelper.SetActive(false);
                RacketParam.transform.localPosition = new Vector3(99, 99, 99);

            }
            else if (OVRInput.GetDown(OVRInput.RawButton.X))
            {
                On = true;
                UIhelper.SetActive(true);
                RacketParam.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
        
       
    }
}
