using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{


    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public bool hideCrosshair;
    public bool locked;
    [SerializeField] private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        hideCrosshair = false;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Input.mousePosition;
       
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit) && !locked)
        {
            transform.position = raycastHit.point;
        }
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            locked = true;
        }

       // if (Input.GetKeyDown(KeyCode.F) && locked)
            //locked = false;*/
    }
}
