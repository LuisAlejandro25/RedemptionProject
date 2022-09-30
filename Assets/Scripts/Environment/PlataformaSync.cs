using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlataformaSync : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasRef;
    [SerializeField]
    private GameObject arrowRef;
    [SerializeField]
    private GameObject teclaRef;
    public bool firstPlatform;
    public bool singelUse;
    public UnityEvent OnPlayerInteract;
    private GameObject playerRef;
    // Start is called before the first frame update
    void Start()
    {
        canvasRef.SetActive(false);
        if(firstPlatform){
            canvasRef.SetActive(true);
            arrowRef.SetActive(true);
        }
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /*if(canvasRef.activeInHierarchy){
            canvasRef.transform.LookAt(playerRef.transform.position);
            canvasRef.transform.localRotation = new Vector3(canvasRef.transform.localRotation.x, 90f * Mathf.Deg2Rad, 90f * Mathf.Deg2Rad) ;
        }*/
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            if(firstPlatform)
                arrowRef.SetActive(false);
            //teclaRef.SetActive(true);
            if(singelUse){
                OnPlayerInteract.Invoke();
                singelUse = false;
            }
        }
    }

    void OnTriggerExit(Collider col){
        if(col.CompareTag("Player")){
            if(firstPlatform)
                arrowRef.SetActive(true);
            //teclaRef.SetActive(false);
        }
    }
}
