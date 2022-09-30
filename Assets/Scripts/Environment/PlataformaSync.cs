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
    private bool playerIn;
    // Start is called before the first frame update
    void Start()
    {
        canvasRef.SetActive(false);
        if(firstPlatform){
            canvasRef.SetActive(true);
            arrowRef.SetActive(true);
        }
        playerIn = false;
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIn && Input.GetKeyDown(KeyCode.Q) && singelUse){
            OnPlayerInteract.Invoke();
            singelUse = false;
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            if(firstPlatform)
                arrowRef.SetActive(false);
            teclaRef.SetActive(true);
            playerIn = true;
        }
    }

    void OnTriggerExit(Collider col){
        if(col.CompareTag("Player")){
            if(firstPlatform)
                arrowRef.SetActive(true);
            teclaRef.SetActive(false);
            playerIn = false;
        }
    }
}
