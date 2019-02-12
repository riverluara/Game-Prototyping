using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpOrDown : MonoBehaviour {
    private float totalTime = 0.0f;
    private Vector3 originalPosition;
    public float moveSpeed = 1.0f;
    public float maxPosition = 3.0f;
    // Use this for initialization
    void Start () {
        originalPosition = this.transform.position;

    }

    // Update is called once per frame
    void Update () {
        totalTime = totalTime + 0.01f;
        this.transform.position = originalPosition + Vector3.up * maxPosition * Mathf.Sin(totalTime * moveSpeed);
    }
}
