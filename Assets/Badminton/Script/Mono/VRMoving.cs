using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class VRMoving : MonobitEngine.MonoBehaviour
{
    private Transform playerRig;
    private Transform HEADRig;
    private Transform LhandRig;
    private Transform RhandRig;



    public Transform Head;
    // public Transform body;
    public Transform Lhand;  //本格アバターを導入後に反映する
    public Transform Rhand;  //本格アバターを導入後に反映する
    private MonobitView monobitview;
    // Start is called before the first frame update
    void Start()
    {
        monobitview = GetComponent<MonobitView>();

        GameObject cameraObject = GameObject.Find("OVRPlayerController");
        playerRig = cameraObject.transform;
        Debug.Log("playerRig" + playerRig.transform.position);
        HEADRig = cameraObject.transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor").gameObject.transform;
        LhandRig = cameraObject.transform.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor").gameObject.transform;
        RhandRig = cameraObject.transform.Find("OVRCameraRig/TrackingSpace/RightHandAnchor").gameObject.transform;

        //本格アバターを導入後に反映する
        if(monobitView.isMine)
         {
            
             foreach (var item in GetComponentsInChildren<Renderer>())
             {
                 item.enabled = false;
             }
             foreach (var item2 in GetComponentsInChildren<Canvas>())
             {
                 item2.enabled = false;
             }
            
         }
    }
    // Update is called once per frame
    void Update()
    {

        //変更点8・2
        //新しいトランスフォームの値を代入する
        if (monobitview.isMine)
        {
            // MapPosition(body, playerRig);
            MapPosition(Head, HEADRig);
            MapPosition(Lhand, LhandRig);  //本格アバターを導入後に反映する
            MapPosition(Rhand, RhandRig);  //本格アバターを導入後に反映する


            monobitView.RPC("MapPotisions", MonobitTargets.OthersBuffered,HEADRig.transform.position, HEADRig.transform.rotation, LhandRig.transform.position, LhandRig.transform.rotation, RhandRig.transform.position, RhandRig.transform.rotation);
        }
    }

    void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }

    [MunRPC]
    public void MapPotisions(Vector3 Headpos, Quaternion Headrot, Vector3 Lhandpos, Quaternion Lhandrot, Vector3 Rhandpos, Quaternion Rhandrot )
    {
        Head.position = Headpos;
        Head.rotation = Headrot;
        Lhand.position = Lhandpos;
        Lhand.rotation = Lhandrot;
        Rhand.position = Rhandpos;
        Rhand.rotation = Rhandrot;
    }
}
