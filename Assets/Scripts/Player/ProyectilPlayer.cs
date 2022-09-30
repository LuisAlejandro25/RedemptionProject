using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilPlayer : MonoBehaviour
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
        if(col.CompareTag("Meele")){
            col.GetComponent<DemonMeele>().GetHurt();
            Destroy(this.gameObject);
        }

        if(col.CompareTag("Archer")){
            col.GetComponent<DemonArcher>().GetHurt();
            Destroy(this.gameObject);
        }
    }
}
