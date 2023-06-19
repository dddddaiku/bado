using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFix : MonoBehaviour
{
    public GameObject Rhand;
    public Rigidbody rb;
    public Vector3 NowBoxPos;
    public Quaternion NowBoxRot;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        NowBoxPos = transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        NowBoxRot = Rhand.transform.rotation;
        
        rb.MoveRotation(NowBoxRot);
        transform.localPosition = NowBoxPos;
    }
    
}
