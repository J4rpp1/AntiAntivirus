using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{


    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public bool hideCrosshair;
    public GameObject crosshair;

    void Start()
    {
        Cursor.visible = false;
        hideCrosshair = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
   

    }
}
