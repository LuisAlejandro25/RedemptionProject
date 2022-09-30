using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformasScript : MonoBehaviour
{
    public bool dissolve = false;
    public bool movement = false;
    public bool playFromStart = false;
    private Vector3 startPosition;
    [SerializeField]
    private Transform pos1;
    [SerializeField]
    private Transform pos2;
    [SerializeField]
    private BoxCollider colliderRef;
    [SerializeField]
    private BoxCollider triggerRef;
    private MeshRenderer renderRef;
    public float timeDissolve;
    public float speedMovement = 5.0f;
    private float alpha = 0.0f;
    private bool alphaFlag = false;
    private Vector3 initPos;
    private Vector3 nextPos;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        //colliderRef = GetComponent<BoxCollider>();
        renderRef = GetComponent<MeshRenderer>();
        if(dissolve){
            renderRef.material.color = new Color(renderRef.material.color.r,
                                                 renderRef.material.color.g,
                                                 renderRef.material.color.b,
                                                 0.0f);
            colliderRef.enabled = false;
        }

        if(!movement && triggerRef != null){
            triggerRef.enabled = false;
        }
        
        //if(movement && playFromStart){
        //    nextPos = pos1.position;
        //    StartCoroutine(Move());
        //}

        //if(dissolve && playFromStart)
        //    StartCoroutine(Appear());
    }

    // Update is called once per frame
    void Update()
    {
        if(alphaFlag && speedMovement > 0f){
            alpha += Time.deltaTime / speedMovement;
            Mathf.Clamp(alpha, 0.0f, 1.0f);
        }
    }

    IEnumerator Move(){
        initPos = this.transform.position;
        alpha = 0.0f;
        alphaFlag = true;
        while(Vector3.Distance(this.transform.position, nextPos) > 0f){
            this.transform.position = Vector3.Lerp(initPos, nextPos, alpha);
            yield return null;
        }
        alphaFlag = false;
        StartCoroutine(SelectNewPos());
    }

    IEnumerator SelectNewPos(){
        if(playFromStart){
            if(Vector3.Distance(this.transform.position, startPosition) < 0.5f)
                nextPos = pos1.position;
            else
                nextPos = startPosition;
        }else{
            if(Vector3.Distance(this.transform.position, pos1.position) < 0.1f)
                nextPos = pos2.position;
            else
                nextPos = pos1.position;
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Move());
    }

    IEnumerator Appear(){
        renderRef.material.color = new Color(renderRef.material.color.r,
                                                 renderRef.material.color.g,
                                                 renderRef.material.color.b,
                                                 1.0f);
        colliderRef.enabled = true;
        yield return new WaitForSeconds(timeDissolve);
        StartCoroutine(Disappear());
        
    }

    IEnumerator Disappear(){
        renderRef.material.color = new Color(renderRef.material.color.r,
                                                 renderRef.material.color.g,
                                                 renderRef.material.color.b,
                                                 0.0f);
        colliderRef.enabled = false;
        yield return new WaitForSeconds(timeDissolve/3f);
        StartCoroutine(Appear());
    }

    public void StartMovement(){
        nextPos = pos1.position;
        StartCoroutine(Move());
    }

    public void StopMovement(){
        StopAllCoroutines();
        alphaFlag = false;
        alpha = 0.0f;
    }

    public void StartDisolve(){
        StartCoroutine(Appear());
    }

    public void StartInerseDisolve(){
        StartCoroutine(Disappear());
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            col.gameObject.transform.parent = this.transform;
        }
    }

    void OnTriggerExit(Collider col){
        if(col.CompareTag("Player")){
            col.gameObject.transform.parent = null;
        }
    }
}
