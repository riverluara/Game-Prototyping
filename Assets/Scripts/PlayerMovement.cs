using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //public GameObject playerJelly;
    // Use this for initialization
    public Camera mainCamera;
    public float forceAmount = 5.0f;

    private Rigidbody playerRb;
    private Transform currentPosition;
    private Vector3 bounceDirection;
    private bool isClikingBall = false;
    
	void Start () {
        playerRb = this.GetComponent<Rigidbody>();
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
        
        Debug.Log(Input.mousePosition);
        Debug.Log(location);
        bounceDirection = this.transform.position - location;
        Debug.Log(bounceDirection);
        isClikingBall = false;
        playerRb.AddForce(bounceDirection * forceAmount, ForceMode.Impulse);
           
    }
}
