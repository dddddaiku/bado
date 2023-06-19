using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerShotParm : MonoBehaviour
{
    public Text[]  numTex;
    public GameObject Manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Zero(bool Up)
    {
        if (Up)
        {
            Manager.GetComponent<PhysicsCulc>().Num[0] += 1;
            numTex[0].text = "" + Manager.GetComponent<PhysicsCulc>().Num[0];
        }
        if (!Up)
        {
            Manager.GetComponent<PhysicsCulc>().Num[0] -= 1;
            numTex[0].text = "" + Manager.GetComponent<PhysicsCulc>().Num[0];
        }
    }
    public void One(bool Up)
    {
        if (Up)
        {
            Manager.GetComponent<PhysicsCulc>().Num[1] += 1;
            numTex[1].text = "" + Manager.GetComponent<PhysicsCulc>().Num[1];
        }
        if (!Up)
        {
            Manager.GetComponent<PhysicsCulc>().Num[1] -= 1;
            numTex[1].text = "" + Manager.GetComponent<PhysicsCulc>().Num[1];
        }
    }
    public void Two(bool Up)
    {
        if (Up)
        {
            Manager.GetComponent<PhysicsCulc>().Num[2] += 1;
            numTex[2].text = "" + Manager.GetComponent<PhysicsCulc>().Num[2];
        }
        if (!Up)
        {
            Manager.GetComponent<PhysicsCulc>().Num[2] -= 1;
            numTex[2].text = "" + Manager.GetComponent<PhysicsCulc>().Num[2];
        }
    }
    public void Three(bool Up)
    {
        if (Up)
        {
            Manager.GetComponent<PhysicsCulc>().Num[3] += 1;
            numTex[3].text = "" + Manager.GetComponent<PhysicsCulc>().Num[3];
        }
        if (!Up)
        {
            Manager.GetComponent<PhysicsCulc>().Num[3] -= 1;
            numTex[3].text = "" + Manager.GetComponent<PhysicsCulc>().Num[3];
        }
    }
    public void Four(bool Up)
    {
        if (Up)
        {
            Manager.GetComponent<PhysicsCulc>().Num[4] += 1;
            numTex[4].text = "" + Manager.GetComponent<PhysicsCulc>().Num[4];
        }
        if (!Up)
        {
            Manager.GetComponent<PhysicsCulc>().Num[4] -= 1;
            numTex[4].text = "" + Manager.GetComponent<PhysicsCulc>().Num[4];
        }
    }
}
