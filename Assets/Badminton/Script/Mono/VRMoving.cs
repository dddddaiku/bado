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
    public Transform Lhand;  //�{�i�A�o�^�[�𓱓���ɔ��f����
    public Transform Rhand;  //�{�i�A�o�^�[�𓱓���ɔ��f����
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

        //�{�i�A�o�^�[�𓱓���ɔ��f����
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

        //�ύX�_8�E2
        //�V�����g�����X�t�H�[���̒l��������
        if (monobitview.isMine)
        {
            // MapPosition(body, playerRig);
            MapPosition(Head, HEADRig);
            MapPosition(Lhand, LhandRig);  //�{�i�A�o�^�[�𓱓���ɔ��f����
            MapPosition(Rhand, RhandRig);  //�{�i�A�o�^�[�𓱓���ɔ��f����


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
