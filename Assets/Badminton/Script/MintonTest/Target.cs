using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public GameObject OutLine, Player;
    public Text Scoretext, LengthText;
    public int GetPoint;
         [SerializeField] float OutlineScale,Count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Outline();
        Distance();
    }
    public void Outline()
    {
        Count += Time.deltaTime * 0.2f;
        if (Count > 1.2f)
        {
            Count = 1f;
        }
        OutLine.transform.localScale = new Vector3(Count, Count, 0);
    }
    public void Distance()
    {
        
        LengthText.text = "距離：" + Vector2.Distance(new Vector2(transform.position.x, transform.position.z),new Vector2( Player.transform.position.x, Player.transform.position.z)).ToString("f1") + "m";

    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("bole"))
        {
            GetPoint++;
            Scoretext.text = "スコア：" + GetPoint;
            float X = Random.Range(-5, 5);
            float Z = Random.Range(0,5);
            transform.position = new Vector3(X, transform.position.y, Z);

        }
    }
    

}
