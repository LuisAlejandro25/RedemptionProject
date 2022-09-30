using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    private bool canDamage = false;
    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player") && canDamage){
            canDamage = false;
            col.GetComponent<PlayerMovement>().GetHurt();
        }
    }

    public void SetCanDamage(bool newBool){
        canDamage = newBool;
    }
}
