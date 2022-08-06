using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript: MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController charControl;
    private Vector3 dir;
    public int speed = 15;
    private int currentLane = 0;
    public float distanceOfLane = 5f;
    private Vector3 newLocation;
    private float gravity = -20f;
    public Animator animator;
    private bool increaseSpeed = true;
    public static float zDisplacement;
    public static Vector3 playerPosition;
    public static bool isMagnet;
    
    
    
    void Start()
    {
        charControl = GetComponent<CharacterController>();
        isMagnet = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = transform.position;
        zDisplacement = transform.position.z;
        //set speed in forward direction to the speed variable
        dir.z = speed;

        //allow the increase of speed 1 tile before every time it's changed avoids double call of function
        if (((GameManager.tilesSpawned + 1) % 20) == 0) {
            increaseSpeed = true;
        }

        //increase speed every 20 tiles spawned
        if ((GameManager.tilesSpawned % 20) == 0 && increaseSpeed) {
            speed += 3;
            increaseSpeed = false;
        }

        //if the player is in the air apply gravity
        if (!charControl.isGrounded)
            dir.y += gravity * Time.deltaTime;

        //the animator paramater is set according to the isGrounded attribute of the character controller
        animator.SetBool("isGrounded", charControl.isGrounded);



        //only when in game
        if (GameStateManager.gameState == 1)
        {   
            //allow player to jump when on the ground and prevent them from jumping when in the air
            if (charControl.isGrounded)
            {
                dir.y = -1;

                //jump when the buttons are pressed and change animation and play the jump sound
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                    animator.SetBool("isGrounded", charControl.isGrounded);
                    FindObjectOfType<AudioManager>().PlaySound("Jump");
                }
            }

            //if the player is in the air a down arrow press allows for slam by increasing gravity
            if (!charControl.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    dir.y = -25;
                }
            }
            //left and right buttons change the lane in which the player is meant to be
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                currentLane++;

                if (currentLane == 2)
                    currentLane = 1;

            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                currentLane--;

                if (currentLane == -2)
                    currentLane = -1;
            }
            //the new location calculates the z and y coordinates of the runner
            newLocation = transform.position.z * transform.forward + transform.position.y * transform.up;

            //changes the new location based on the which lane the player is required to be in
            if (currentLane == -1)
            {
                newLocation += Vector3.left * distanceOfLane;

            }

            if (currentLane == 1)
            {
                newLocation += Vector3.right * distanceOfLane;

            }



            if (transform.position != newLocation)
            {
                Vector3 difference = newLocation - transform.position;
                Vector3 moveDirection = difference.normalized * 25 * Time.deltaTime;
                if (moveDirection.sqrMagnitude < difference.sqrMagnitude)
                    charControl.Move(moveDirection);
                else
                    charControl.Move(difference);
            }
        }
            charControl.Move(dir * Time.deltaTime);

        if (GameStateManager.gameState == 2) {
            charControl.Move(-dir * Time.deltaTime);
        }
        } 

    //changes the magnitude of the value the player is meant to travel in on the y axis
    private void Jump() 
    {

        dir.y = 12;
        
        
    }

    //collider control - if player hits an object tagged as obstacle the game state is changed to 2 and the game over sound is played
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            if(GameStateManager.gameState == 1)
            FindObjectOfType<AudioManager>().PlaySound("gameOver");
            GameStateManager.gameState = 2;
        }
    }

    //if the player collides with an object tagged magnet the powerup coroutine is started and the sound is played
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Magnet") 
        {
            StartCoroutine(EnablePowerup());
            FindObjectOfType<AudioManager>().PlaySound("magnetPickup");
        }
    }

    //the coroutine activates the magnet powerup by changing the boolean for 7 seconds
    IEnumerator EnablePowerup()
    {

       
        isMagnet = true;
        yield return new WaitForSeconds(7.0f);
        isMagnet = false;
    
    }
  
}
