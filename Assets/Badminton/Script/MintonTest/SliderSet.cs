using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SliderSet : MonoBehaviour
{
    public GameObject Racket, shuttle;
    public float MaxRacketGram, MaxRacketCor;
    public float MaxShuttleGram, MaxShuttleCor, MaxShuttleDrag;
    public float ShuttoleGram, ShuttoleDrag, ShuttoleCor;
    // Start is called before the first frame update
    void Start()
    {
        //ShuttoleGram = 5;
        //ShuttoleDrag = 2;
        //ShuttoleCor = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Rgram(float Val)
    {
        Racket.GetComponent<HitSpot>().RacketGram =  Val;
    }
    public void RCor(float Val)
    {
        Racket.GetComponent<HitSpot>().RacketCor =  Val;
    }
    public void Sgram(float Val)
    {
        ShuttoleGram = Val;
        shuttle.GetComponent<shuttleInstance>().ShuttoleGram = Val;
    }
    public void SCor(float Val)
    {
        ShuttoleCor = Val;
        //shuttle.GetComponent<shuttleInstance>().ShuttoleCor =  Val;
    }
    public void SDrag(float Val)
    {
        ShuttoleDrag = Val;
        shuttle.GetComponent<shuttleInstance>().ShuttoleDrag =  Val;
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("Room1");
    }
}
