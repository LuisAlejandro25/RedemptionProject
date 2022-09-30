using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class DeathZone : MonoBehaviour
{
    public UnityEvent OnPlayerFall;
    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            OnPlayerFall.Invoke();
            //StartCoroutine(RestartGame());
        }
    }

    IEnumerator RestartGame(){
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);
    }
}
