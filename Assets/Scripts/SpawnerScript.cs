using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    private bool used = false;
    public GameObject[] enemyRef;
    public Transform[] spawnPos;
    private GameObject aux;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player") && !used){
            used = true;
            SpawnEnemys();
        }
    }

    private void SpawnEnemys(){
        for(int i= 0; i < enemyRef.Length; i++){
            aux = Instantiate(enemyRef[i], spawnPos[i].position, Quaternion.identity);
            aux.transform.forward = spawnPos[i].forward;
        }

    }
}
