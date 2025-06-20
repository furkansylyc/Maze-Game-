using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    public UnityEngine.UI.Text zaman, can,durum;
    public UnityEngine.UI.Button btn;
    private Rigidbody rg;
    public float Hiz=1.8f;
    float zamanSayaci = 25f;
    int canSayaci = 3;
    bool oyunDevam = true;
    bool oyunTamam = false;
   
    void Start()
    {
        rg = GetComponent<Rigidbody>();       
    }


    void Update()
    {
        if (oyunDevam && !oyunTamam) { 
        zamanSayaci -= Time.deltaTime; //her saniyede 1 düþürür
        zaman.text = (int)zamanSayaci + "";
        }else if (!oyunTamam)
        {
            durum.text = "Oyun Tamamlanamadý.";
            btn.gameObject.SetActive(true);
            
        }
        if (zamanSayaci < 0)
        { 
        oyunDevam = false;
        durum.text = "Oyun Tamamlanamadý";
            zaman.text = "0";
            btn.gameObject.SetActive(true);
           
        }
    }

    private void FixedUpdate()
    {
        if (oyunDevam == true && !oyunTamam)
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-dikey, 0, yatay);
            rg.AddForce(kuvvet * Hiz);
        }
        else
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        string objad= collision.gameObject.name;
        if (objad.Equals("bitis"))
        {
            oyunTamam= true;
            durum.text = "Bravo! Oyun Tamamlandý";
            btn.gameObject.SetActive(true);
            GetComponent<AudioSource>().Play();
        }
        else if(!objad.Equals("kucukzemin") && !objad.Equals("bitiscizgisi"))
        {
            canSayaci = canSayaci - 1;
            can.text = canSayaci + "";
            if (canSayaci == 0)
            {
                oyunDevam = false;
                durum.text = "Oyun Tamamlanamadý";
                can.text = "0";
                btn.gameObject.SetActive(true);
                
            }
        }
        
    }
}

