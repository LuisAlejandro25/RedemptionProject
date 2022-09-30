using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class DemonArcher : MonoBehaviour
{
    private float health = 3.0f;

    private Animator demonAnim;
    private NavMeshAgent agent;
    public Transform[] targets;
    private Vector3 destination;
    private int index = 0;
    private EnemyState currentState;
    private GameObject playerRef;

    //Fire
    private bool attacked;
    private float fireRate = 2.5f;
    private float timeRate = 3.0f;
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private Transform shootRef;
    private GameObject arrowAux;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        demonAnim = GetComponent<Animator>();
        attacked = false;
        StateMachine(EnemyState.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        demonAnim.SetFloat("speed", agent.velocity.sqrMagnitude);
        if(agent.remainingDistance < 0.5f){
            agent.isStopped = true;
            if(health > 0f && currentState == EnemyState.PATROL)
                StateMachine(EnemyState.IDLE);
        }

        if(attacked){
            timeRate += Time.deltaTime;
            if(timeRate > fireRate)
                attacked = false;
        }
    }

    public void StateMachine(EnemyState newState)
    {
        //Debug.Log(newState);
        StopAllCoroutines();
        currentState = newState;
        switch (currentState)
        {
            case EnemyState.IDLE:
                StartCoroutine(IdleBehiaviour());
                break;
            case EnemyState.PATROL:
                Patrol();
                break;
            //case EnemyState.DAMAGE:
            //    SetStateToDamage();
            //    break;
            case EnemyState.DEATH:
                Death();
                break;
            case EnemyState.ATTACK:
                StartCoroutine(Attack());
                break;
        }
    }

    private IEnumerator IdleBehiaviour(){
        agent.isStopped = true;
        yield return new WaitForSeconds(3.0f);
        StateMachine(EnemyState.PATROL);
    }

    private void Patrol()
    {  
        if(targets.Length > 0){      
            destination = targets[index].position;
            agent.SetDestination(destination);
            agent.isStopped = false;
            index++;
            if (index >= targets.Length)
                index = 0;
        }
    }

    private IEnumerator Attack(){
        agent.isStopped = true;
        if(playerRef!=null)
            this.transform.LookAt(playerRef.transform.position);
        yield return new WaitForSeconds(fireRate);
        demonAnim.SetTrigger("attack");
        StateMachine(EnemyState.ATTACK);        
    }

    private void Death(){
        Debug.Log("Muerto");
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            playerRef = col.gameObject;
            StateMachine(EnemyState.ATTACK);
        }
    }

    void OnTriggerExit(Collider col){
        if(col.CompareTag("Player")){
            playerRef = null;
            StateMachine(EnemyState.IDLE);
        }
    }

    public void InstantiateArrow(){
        if(playerRef != null){
            shootRef.LookAt(playerRef.transform.position + (Vector3.up * 1.4f));
            arrowAux = Instantiate(arrowPrefab, shootRef.position,shootRef.rotation);
        }
    }
}

public enum EnemyState
{ 
    IDLE,
    PATROL,
    ATTACK,
    DAMAGE,
    DEATH
}
