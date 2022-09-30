using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position,GroundDistance,Ground);
        moveDir = Vector3.zero;
        
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + camRef.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRB.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        if (Input.GetButtonDown("Fire1")){
            //Debug.Log("Fire!!!");
        }
    }

    void FixedUpdate(){
        playerRB.velocity = (moveDir * speed) + (playerRB.velocity.y * Vector3.up);
    }
}
