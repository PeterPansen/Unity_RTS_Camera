using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RTSCamera : MonoBehaviour
{


    [Header("Camera Movement")]
    public float moveSpeed = 300.0f;
    public float drag = 5.0f;
    public float mass = 0.25f;

    [Header("Camera Rotation")]
    public float turnSpeed = 50.0f;
    public float angular_drag = 14.0f;

    [Header("Restrictions")]
    public float minHeight = 0.0f;
    public float maxHeight = 100.0f;

    [Header("Zoom")]
    public float zoomSpeed = 40.0f;
    public float zoomDrag = 10.0f;


    private Transform parent;
    private Rigidbody rBodyParent;
    private float currentZoomSpeed = 0.0f;


    // Use this for initialization
    void Start()
    {

        if (this.transform.parent == null)
        {
            parent = new GameObject().transform;
            parent.name = "Camera_Parent";
            parent.transform.position = this.transform.position;
            this.transform.parent = parent.transform;
        }
        else
        {
            parent = this.transform.parent;
        }

        rBodyParent = parent.transform.GetComponent<Rigidbody>();

        if (!rBodyParent)
        {
            rBodyParent = parent.gameObject.AddComponent<Rigidbody>();
        }

        rBodyParent.useGravity = false;
        rBodyParent.angularDrag = angular_drag;
        rBodyParent.drag = drag;
        rBodyParent.mass = mass;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetForce = Vector3.zero;
        float turnVector = 0.0f;

        if (Input.GetKey("w"))
        {
            targetForce.z += moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s"))
        {
            targetForce.z -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a"))
        {
            targetForce.x -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey("d"))
        {
            targetForce.x += moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey("q"))
        {
            turnVector -= turnSpeed;
        }

        if (Input.GetKey("e"))
        {
            turnVector += turnSpeed;
        }


        rBodyParent.AddTorque(new Vector3(0, turnVector, 0));

        rBodyParent.AddRelativeForce(targetForce, ForceMode.Impulse);

        currentZoomSpeed += Input.mouseScrollDelta.y * zoomSpeed;



        //Prevent Jittering at height borders
        //Negative goes up, Positive goes down
        if (this.transform.position.y >= maxHeight && currentZoomSpeed < 0 || this.transform.position.y <= minHeight && currentZoomSpeed > 0)
        {
            currentZoomSpeed = 0;
        }

        //If currentZoomSpeed is closer to zero than one drag-Value we simply set it to zero
        if (Mathf.Abs(currentZoomSpeed) < zoomDrag)
        {
            currentZoomSpeed = 0;
        }

        //Current Zoom Speed is Negative
        if (currentZoomSpeed < 0)
        {
            currentZoomSpeed += zoomDrag;
        }

        //Current Zoom Speed is Positive
        else if (currentZoomSpeed > 0)
        {
            currentZoomSpeed -= zoomDrag;
        }

        if (currentZoomSpeed != 0)
        {
            //this.transform.Translate(Vector3.forward * currentZoomSpeed * Time.deltaTime, Space.Self);
            this.parent.transform.Translate(this.transform.forward * currentZoomSpeed * Time.deltaTime, Space.World);
        }



        this.transform.position = new Vector3(this.transform.position.x, Mathf.Clamp(this.transform.position.y, minHeight, maxHeight), this.transform.position.z);

    }
}
