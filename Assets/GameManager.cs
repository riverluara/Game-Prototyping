using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    // Use this for initialization
    public float moveSpeed = 2.0f;
    public GameObject player;
    public Text healthText;
    public Text loseText;
    private bool isRow = true;
    private Rigidbody playerRb;
    public static bool isFirstSawtooth = true;
    [SerializeField]private int playerHealth = 5;
	void Start () {
        playerRb = player.GetComponent<Rigidbody>();
        healthText.text = playerHealth.ToString();
        loseText.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        Move(isRow);
        PlayerMovement.CollisionRowHappened += MoveRightOrLeft;
        PlayerMovement.CollisionColumnHappened += MoveUpOrDown;

        PlayerMovement.SawtoothDamageHappened += GetHurt;
       
        if(playerHealth <= 0)
        {
            loseText.gameObject.SetActive(true);
        }
	}
    void MoveRightOrLeft()
    {
        isRow = true;
       

    }
    void MoveUpOrDown()
    {
        isRow = false;
        

    }
    void Move(bool isRow)
    {
        if(isRow == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
               
                player.transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.D))
            {

                player.transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            }
        }
        if(isRow == false)
        {
            if (Input.GetKey(KeyCode.W))
            {

                player.transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.S))
            {

                player.transform.position += Vector3.down * moveSpeed * Time.deltaTime;

            }
        }
        

    }
    void GetHurt(Vector3 currentPosition)
    {
        Debug.Log(playerHealth);
        
        
        if (isFirstSawtooth)
        {
            playerHealth--;
            healthText.text = playerHealth.ToString();
            MoveBack(currentPosition);
        }
        isFirstSawtooth = false;
        
    }

    void MoveBack(Vector3 currentPosition)
    {
        player.transform.position = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z);

    }
}
