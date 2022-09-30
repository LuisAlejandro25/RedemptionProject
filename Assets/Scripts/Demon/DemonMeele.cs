using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonMeele : MonoBehaviour
{
    private float health = 3.0f;

    private Animator demonAnim;
    private NavMeshAgent agent;
    public Transform[] targets;
    private Vector3 destination;
    private int index = 0;
    private EnemyState currentState;
    private GameObject playerRef;

    [SerializeField]
    private SwordScript swordRef;
    private bool attacked;
    private float fireRate = 2.5f;
    private float timeRate = 3.0f;
    [SerializeField]
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        demonAnim = GetComponent<Animator>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        attacked = false;
        demonAnim.SetTrigger("taunt");
        StateMachine(EnemyState.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        demonAnim.SetFloat("speed", agent.velocity.sqrMagnitude);

        if(currentState == EnemyState.PATROL){
            if(playerRef!=null)
                agent.SetDestination(playerRef.transform.position);
            
            if(agent.remainingDistance < 2.0f){
                agent.isStopped = true;
                StateMachine(EnemyState.ATTACK);
            }
        }

        if(currentState == EnemyState.ATTACK){
            if(playerRef!=null)
                agent.SetDestination(playerRef.transform.position);
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
                if(agent.remainingDistance < 2.0f)
                    StartCoroutine(Attack());
                else
                    StateMachine(EnemyState.PATROL);
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
        if(playerRef != null){
            agent.SetDestination(playerRef.transform.position);  
            agent.isStopped = false;
        }
        /*if(targets.Length > 0){      
            destination = targets[index].position;
            agent.SetDestination(destination);
            agent.isStopped = false;
            index++;
            if (index >= targets.Length)
                index = 0;
        }*/
    }

    private IEnumerator Attack(){
        agent.isStopped = true;
        if(playerRef!=null)
            this.transform.LookAt(playerRef.transform.position);
        demonAnim.SetTrigger("attack");
        yield return new WaitForSeconds(fireRate);
        StateMachine(EnemyState.ATTACK);        
    }

    private void Death(){
        Debug.Log("Muerto");
    }

    public void ActivateSword(){
        swordRef.SetCanDamage(true);
    }

    public void DeactivateSword(){
        swordRef.SetCanDamage(false);
    }

    public void Grito(){
        audioSource.Play();
    }
}