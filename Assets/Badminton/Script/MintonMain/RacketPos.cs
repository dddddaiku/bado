using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacketPos : MonoBehaviour
{
    public GameObject Rhand,Lhand;
    public Rigidbody rb;
    public Vector3 NowRacketPos, Racketacceleration;
    public Quaternion NowRacketRot;
    public bool LeftHandUse;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!LeftHandUse)
        {
            NowRacketPos = Rhand.transform.position;
            NowRacketRot = Rhand.transform.rotation;
            rb.MovePosition(NowRacketPos);
            rb.MoveRotation(NowRacketRot);
        }
        else
        {
            NowRacketPos = Lhand.transform.position;
            NowRacketRot = Lhand.transform.rotation;
            rb.MovePosition(NowRacketPos);
            rb.MoveRotation(NowRacketRot);
        }
        
       
    }
    
}
