using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private float health = 10.0f;


    private Rigidbody playerRB;
    [SerializeField]
    private Transform camRef;
    public float speed = 5f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Vector3 moveDir;
    private float DashDistance = 5f;
    public float jumpForce = 1f;

    //Ground Checker
    public float GroundDistance = 0.1f;
    public LayerMask Ground;
    private bool isGrounded = true;
    [SerializeField]
    private Transform groundChecker;

    [SerializeField]
    private Animator animRef;
    [SerializeField]
    private GameObject projectileRef;
    [SerializeField]
    private Transform shootPos;
    private GameObject auxGamebject;

    public UnityEvent OnDeath;

    private bool dead = false;

    private bool disableMovement = false;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody>();
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(dead || disableMovement)
            return;
        isGrounded = Physics.CheckSphere(groundChecker.position,GroundDistance,Ground);
        moveDir = Vector3.zero;
        
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;
        animRef.SetFloat("speed", direction.magnitude);

        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + camRef.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            StartCoroutine(ApplyJumpForce());
            animRef.SetTrigger("Jump");
        }

        if (Input.GetButtonDown("Fire1")){
            animRef.SetTrigger("attack");
            StartCoroutine(InstantiateProjectile());
        }
    }

    void FixedUpdate(){
        playerRB.velocity = (moveDir * speed) + (playerRB.velocity.y * Vector3.up);
    }

    IEnumerator ApplyJumpForce(){
        yield return new WaitForSeconds(0.02f);
        playerRB.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }

    IEnumerator InstantiateProjectile(){
        yield return new WaitForSeconds(1f);
        auxGamebject = Instantiate(projectileRef, shootPos.position, Quaternion.identity);
        auxGamebject.transform.forward = shootPos.transform.forward; 
    }

    public void GetHurt(){
        health -= 1.0f;
        if(health < 0 && !dead){
            dead = true;
            animRef.SetTrigger("dead");
            OnDeath.Invoke();
        }
    }

    public void DisableMovement(){
        disableMovement = true;
    }
}
