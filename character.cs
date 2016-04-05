﻿using UnityEngine;
using System.Collections;

public class character : MonoBehaviour {

	Rigidbody2D charRB;

	[Header("CHARACTER MOVEMENT")]

	float hVelocity; // store the directions pressed from -1 to 1.
	[Range(0.01f, 5.0f)]
	public float hScale = .05f;  // our scaling factor for horizontal movement
	[Header("horizontal")]
	float vVelocity; // store the vertical velocity

	[Tooltip("The height of our jump")]
	[Range(0.5f, 20f)]
	public float jumpVal = 1.0f; // the jump velocity, when applied.
	[Range(0.5f, 20f)]
	public float secondJumpVal = 1.5f; // the second jump velocity.
	[Tooltip("Let's us known when Character is on the ground")]
	public bool onGround; // let's us know when character is "on the ground"
	public float interJumpTime = .25f;
	float jumpStartTime;
	public int jumps;

    public bool crawl = false;
   
  
    bool facingRight = true;
    Animator animator;

    public float rotationMin = 15f;
    public float rotationMax = -15f;

    // Use this for initialization


    void Start () {
		charRB = gameObject.GetComponent<Rigidbody2D> ();
		jumps = 0;
        animator = GetComponent<Animator>();
	}
 
	// Update is called once per frame
	void Update () {
		getHorizontal ();
		getJump ();
        getCrawl();
		move();
        rotationCorrection();
	}





	void getHorizontal(){
		hVelocity = Input.GetAxis ("Horizontal") * hScale * Time.deltaTime;
        animator.SetFloat("hSpeed", Mathf.Abs(hVelocity));
	}

    void getCrawl()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("crawl");
            if (!crawl)
            {
                crawl = true;
                animator.SetBool("crawling", true);
            } else
            {
                crawl = false;
                animator.SetBool("crawling", false);
            }
        }
    }





    void move(){
        if (hVelocity >0 && !facingRight){
            flip();
        }else if (hVelocity <0 && facingRight){
            flip();
        }

        animator.SetBool("onGround", onGround);
        animator.SetFloat("hSpeed",Mathf.Abs(hVelocity*10f));

		charRB.transform.position = new Vector2 (charRB.transform.position.x + hVelocity, charRB.transform.position.y);
		charRB.velocity += (Vector2.up * vVelocity);

        animator.SetFloat("vVelocity", charRB.velocity.y);

       
	}

	void getJump(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(jumps == 1 && ((Time.time - jumpStartTime) > interJumpTime)){
				vVelocity = secondJumpVal;
				jumps++;
			}
			if (onGround) {
				vVelocity = jumpVal;
				jumpStartTime = Time.time;
				jumps++;
			}
		} else {
			vVelocity = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.CompareTag("Ground")){
			if (!onGround) {
				onGround = true;
			}
			jumps = 0;
		}
	}

    void OnTriggerExit2D(Collider2D coll) {
        if (coll.CompareTag("Ground")) {
            if (onGround) {
                onGround = false;
            }
        }
    }
    void flip(){
         facingRight = !facingRight;
          Vector3 theScale = transform.localScale;
          theScale.x *= -1.0f;
          transform.localScale = theScale;
        }
    void rotationCorrection(){
        if (charRB.transform.eulerAngles.z>rotationMax){
            charRB.transform.rotation = Quaternion.Euler(0f, 0f, rotationMax);
        }else if (charRB.transform.eulerAngles.z < rotationMin){
            charRB.transform.rotation = Quaternion.Euler(0f, 0f, rotationMin);
        }
    }
}
