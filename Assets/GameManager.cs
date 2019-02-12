using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    // Use this for initialization
    public float moveSpeed = 1.0f;
    public GameObject player;
    public Text healthText;
    public Text loseText;
    public static bool isFirstSawtooth = true;
    public Text tutorial;
    public GameObject tempParent;
    public Text bonusCount;
    public Text winText;
    public int winBonus = 4;
    [SerializeField] private int playerHealth = 5;
    private bool isRow = true;
    private Rigidbody playerRb;
    private bool isFirstTutorial = true;
    private bool isFirstTouch = true;
    private bool isSwitchTouched = false;
    private int bonus = 0;
	void Start () {

        StartCoroutine(ShowMessageCoroutine("Use A or D To Move Left or Right, Click and Drag to Bounce", 6.0f));
        
        playerRb = player.GetComponent<Rigidbody>();
        healthText.text = playerHealth.ToString();
        bonusCount.text = bonus.ToString();
        loseText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
    
	void Update () {
        
        PlayerMovement.CollisionRowHappened += MoveRightOrLeft;
        PlayerMovement.CollisionColumnHappened += MoveUpOrDown;
        Move(isRow, tempParent);
        PlayerMovement.SawtoothDamageHappened += GetHurt;
        //PlayerMovement.SwitchDown += BonusAdd;
        SwitchMovement.SwitchIsTouched += BonusAdd;
        if (playerHealth <= 0)
        {
            loseText.gameObject.SetActive(true);
        }
        if(bonus >= winBonus)
        {
            winText.gameObject.SetActive(true);
        }
	}
    void BonusAdd(bool switchStatus)
    {
         
        
        if (!switchStatus && !isSwitchTouched)
        {
            bonus++;
            bonusCount.text = bonus.ToString();
            isSwitchTouched = true;

        }
         


        


    }
    void MoveRightOrLeft(Collision collision)
    {

        isRow = true;
        isSwitchTouched = false;
        tempParent = collision.gameObject;

    }
    void MoveUpOrDown(Collision collision)
    {
        //SceneManager.LoadScene("SampleScene");
        if (isFirstTutorial)
        {
            StartCoroutine(ShowMessageCoroutine("Use W or S To Move Up or Down, Be Aware of RED", 6.0f));
            isFirstTutorial = false;
        }
        isRow = false;
        isSwitchTouched = false;
        tempParent = collision.gameObject;

    }
    void Move(bool isRow, GameObject tempParent)
    {
        player.transform.parent = tempParent.transform;
        if(isRow == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
               
                player.transform.localPosition += Vector3.left * moveSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.D))
            {

                player.transform.localPosition += Vector3.right * moveSpeed * Time.deltaTime;

            }
        }
        if(isRow == false)
        {
            if (Input.GetKey(KeyCode.W))
            {

                player.transform.localPosition += Vector3.up * moveSpeed * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.S))
            {

                player.transform.localPosition += Vector3.down * moveSpeed * Time.deltaTime;

            }
        }
        

    }
    void GetHurt(Vector3 currentPosition)
    {
        Debug.Log(playerHealth);

        if (isFirstTouch)
        {
            StopAllCoroutines();
            StartCoroutine(ShowMessageCoroutine("Skip RED Or You Get Hurt", 6.0f));
            isFirstTouch = false;
        }
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

    private IEnumerator ShowMessageCoroutine(string message, float timeToShow)
    {
        yield return new WaitForSecondsRealtime(1.0f);

        //show
        tutorial.enabled = true;
        tutorial.text = message;

        yield return new WaitForSecondsRealtime(timeToShow);

        //hide

        tutorial.enabled = false;
        tutorial.text = "";
    }

}
