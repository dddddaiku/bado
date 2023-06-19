using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotButton : MonoBehaviour
{
    public int Side;
    public GameObject Manager;
    public float Time;
    public bool Wait;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Push()
    {
        
        Manager.GetComponent<MatchManager>().MatchButton(Side);
       
    }

    IEnumerator Waittime(float waittime)
    {
        OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.RTouch);
        yield return new WaitForSeconds(waittime);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);

    }
}
