using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMovement : MonoBehaviour {
    private bool isTouched = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PlayerMovement.UpSwitchHappened += MoveUp;
        PlayerMovement.DownSwitchHappened += MoveDown;
    }
    void MoveUp(string name)
    {
        if (!isTouched && name == this.gameObject.transform.name)
        {
            this.gameObject.transform.position += Vector3.up * 0.3f;
            isTouched = true;
        }
       
    }
    void MoveDown(string name )
    {
        if (!isTouched && name == this.gameObject.transform.name)
        {
            this.gameObject.transform.position += Vector3.down * 0.3f;
            isTouched = true;
        }
            

    }
}
