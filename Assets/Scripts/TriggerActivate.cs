using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerActivate : MonoBehaviour
{
    private bool done;
    public UnityEvent OnEnter;

    // Start is called before the first frame update
    void Start()
    {
        done = false;
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player") && !done){
            done = true;
            OnEnter.Invoke();
        }

    }
}
