using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float speed;
    private float lifeTime = 5.0f;
    private float counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * speed *Time.deltaTime);
        counter += Time.deltaTime;
        if(!(counter <= lifeTime))
            Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            col.GetComponent<PlayerMovement>().GetHurt();
            Destroy(this.gameObject);
        }
    }
}
