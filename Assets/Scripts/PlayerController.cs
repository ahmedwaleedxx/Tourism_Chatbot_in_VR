using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed; //actual speed of movement
    public float rotSpeed; //speed of turning around
    float x; //value on x-axis
    float y; //value on y-axis
    float z; //value on z-axis
    Vector3 moveDirection; //3-dimensional vector to hold player's movement on x,y,z
    Animator anim; //Object of Animator 
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    public KeyCode Spacebar;
    public float jumpHeight = 2;
    public Rigidbody rb;
    public float gravityScale = 5;

    //OVRPointerEventData leftData;
    void Start()
    {
        anim = GetComponent<Animator>();
       // transform.position = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        
        z = Input.GetAxis("Vertical"); //value along the Z-axis
        x = Input.GetAxis("Horizontal"); //value along the X-axis
                                         //y is vertical, so will not change except if character jumps
        moveDirection = new Vector3(x, 0, z);
        //move the character at the given walkspeed through the world space
        transform.Translate(moveDirection * walkSpeed * Time.deltaTime, Space.World);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,
            rotSpeed * Time.deltaTime);
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Swing", true);
        }
        else
        {
            anim.SetBool("Swing", false);
        }

        if (Input.GetKeyDown(Spacebar))
        {

            rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
            //jump();
            print("jump");
        }
    }

    /*    private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag.Equals("Medkit"))
            {
                Destroy(other.gameObject);
            }
        }*/

    void FixedUpdate()
    {
        //GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, jumpHeight, 0);
        //rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Friend" && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        //{
        //    (new NavigationController()).GoToVictoryScene();
        //}
    }
}