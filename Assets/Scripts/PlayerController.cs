using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public float moveSpeed;
    public float speedMultiplier;
    public float speedIncreaseMilestone;
    private float speedMilestoneCount;
    public float jumpForce; 
    public float jumpTime;
    private float jumpTimeCounter;

    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;
    
    // private Collider2D myCollider;

    private Animator myAnimator;

    // Start is called before the first frame update

    public class Store {
        public float moveSpeedS;
        public float speedMultiplierS;
        public float speedIncreaseMilestoneS;
        public float speedMilestoneCountS;
    }

    private Store store;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        // myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        speedMilestoneCount = speedIncreaseMilestone;
        jumpTimeCounter = jumpTime;
        store = new Store {
            moveSpeedS = moveSpeed,
            speedMultiplierS = speedMultiplier,
            speedIncreaseMilestoneS = speedIncreaseMilestone,
            speedMilestoneCountS = speedMilestoneCount
       };
    }

    // Update is called once per frame
    void Update()
    {
        // grounded = Physics2D.IsTouchingLayers(myCollider,whatIsGround);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);


        if(transform.position.x > speedMilestoneCount) {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;

            moveSpeed = moveSpeed * speedMultiplier;
        }


        myRigidbody.velocity = new Vector2(moveSpeed,myRigidbody.velocity.y);

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && grounded == true) {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }

        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) {
            if(jumpTimeCounter > 0) {
                 myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                 jumpTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp(0)) {
            jumpTimeCounter = 0;
        }

        if(grounded) {
            jumpTimeCounter = jumpTime;
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "killbox") {
            gameManager.RestartGame();
            moveSpeed = store.moveSpeedS;
            speedMilestoneCount = store.moveSpeedS;
            speedMultiplier = store.speedMultiplierS;
            speedIncreaseMilestone = store.speedIncreaseMilestoneS;
        }
    }
}
