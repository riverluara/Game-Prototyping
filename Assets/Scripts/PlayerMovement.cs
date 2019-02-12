using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //public GameObject playerJelly;

    // Use this for initialization
    public delegate void CollisionAction(Collision collision);
    public static event CollisionAction CollisionRowHappened;
    public static event CollisionAction CollisionColumnHappened;
    

    public delegate void SwitchCollision(string name);
    public static event SwitchCollision UpSwitchHappened;
    public static event SwitchCollision DownSwitchHappened;

    public delegate void DemageAction(Vector3 currentPosition);
    public static event DemageAction SawtoothDamageHappened;

    public Camera mainCamera;
    public float maxForceAmount = 3.0f;
    public float maxDistance = 2.0f;
    private Rigidbody playerRb;
    private Vector3 currentPosition;
    private Vector3 bounceDirection;
    private bool isClikingBall = false;
    
    
	void Start () {
        playerRb = this.GetComponent<Rigidbody>();
        currentPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {

        //currentPosition.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        if (Input.GetMouseButtonDown(0))
        {
            MouseClick();
        }
        if (Input.GetMouseButtonUp(0) && isClikingBall == true)
        {
            currentPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            GameManager.isFirstSawtooth = true;
            playerRb.useGravity = true;
            PlayerBounce();
            isClikingBall = false;


        }
    }

    void MouseClick()
    {
        Ray ray;
        RaycastHit hit;
       
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != null)
                {
                    
                    if(hit.transform.name == "Player")
                    {
                        isClikingBall = true;
                        Debug.Log(hit.transform.name);
                      
                    
                    }
                    //Debug.Log(hit.transform.tag);
                }

            }
        
    }

    void PlayerBounce()
    {
      
            
        Debug.Log(2);

        Vector3 location = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        location.z = 0.0f;
        float distance = Vector3.Distance(location, this.transform.position);
        float index = Mathf.Clamp(distance / maxDistance, 0.0f, 1.0f);
        Debug.Log(Input.mousePosition);
        Debug.Log(location);
        Debug.Log(distance);
        bounceDirection = this.transform.position - location;
        Debug.Log(bounceDirection);
        isClikingBall = false;
        playerRb.AddForce(bounceDirection * maxForceAmount*index, ForceMode.Impulse);
           
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.tag == "StickyRow")
        {
            FreezeGravity();
            CollisionRowHappened(collision);
        }
        if (collision.transform.tag == "StickyColumn")
        {
            FreezeGravity();
            CollisionColumnHappened(collision);
        }
        if(collision.transform.tag == "SawTooth" )
        {
            Debug.Log("s");
            FreezeGravity();
            SawtoothDamageHappened(currentPosition);
           
        }
        if(collision.transform.tag == "SwitchUp")
        {
            FreezeGravity();
            UpSwitchHappened(collision.transform.name);
        }
        if (collision.transform.tag == "SwitchDown")
        {
            FreezeGravity();
            DownSwitchHappened(collision.transform.name);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
       
            playerRb.useGravity = true;
       
        
    }
    void FreezeGravity()
    {
        playerRb.Sleep();
        playerRb.useGravity = false;

    }
}
