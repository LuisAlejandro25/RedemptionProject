using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * speed *Time.deltaTime);
        //this.transform.localPosition += Vector3.forward * speed *Time.deltaTime;
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            //Debug.Log("Te di puto!!!");
        }
    }
}
