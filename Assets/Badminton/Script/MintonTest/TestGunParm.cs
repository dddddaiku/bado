using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGunParm : MonoBehaviour
{
    public float High, Rad, Power;
    public Text H, R, P;
    public GameObject Gun;

    // Start is called before the first frame update
    void Start()
    {
        H.text = High.ToString() + "m";
        R.text = (-1 * Rad).ToString() + "“x";
        P.text = Power.ToString();
        Gun.transform.position = new Vector3(Gun.transform.position.x, High, Gun.transform.position.z);
        Gun.transform.eulerAngles = new Vector3(Gun.transform.eulerAngles.x, Gun.transform.eulerAngles.y, Rad);
        Gun.GetComponent<shuttleInstance>().ShotPower = Power;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HighUp()
    {
        High += 0.10f;
        H.text = High.ToString() + "m";
        Gun.transform.position = new Vector3(Gun.transform.position.x, High, Gun.transform.position.z);
    }
    public void HighDown()
    {
        High -= 0.10f;
        H.text = High.ToString() + "m";
        Gun.transform.position = new Vector3(Gun.transform.position.x, High, Gun.transform.position.z);
    }
    public void RadUP()
    {
        Rad += 1;
        R.text = (-1 * Rad).ToString() + "“x";
        Gun.transform.eulerAngles = new Vector3(Gun.transform.eulerAngles.x, Gun.transform.eulerAngles.y, Rad);
    }
    public void RadDown()
    {
        Rad -= 1;
        R.text = (-1 * Rad).ToString() + "“x";
        Gun.transform.eulerAngles = new Vector3(Gun.transform.eulerAngles.x, Gun.transform.eulerAngles.y, Rad);
    }
    public void PowerUP()
    {
        Power += 1;
        P.text = Power.ToString();
        Gun.GetComponent<shuttleInstance>().ShotPower = Power;
    }
    public void PowerDown()
    {
        Power -= 1;
        P.text = Power.ToString();
        Gun.GetComponent<shuttleInstance>().ShotPower = Power;
    }
}
