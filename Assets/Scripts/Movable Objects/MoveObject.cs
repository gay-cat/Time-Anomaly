using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private float range_limit;
    [SerializeField] private LayerMask movable;
    private bool objectAttached;
    [SerializeField] private Transform hold_point;
    private Camera cam;
    private GameObject movable_object;
    private Rigidbody movable_object_rb;
    private Collider movable_object_cl;
    // Start is called before the first frame update
    void Start()
    {
        objectAttached = false;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && objectAttached == false)
        {
            Attach();
        }
        else if (Input.GetKeyDown(KeyCode.E) && objectAttached == true)
        {
            Detach();
        }
        Debug.Log(objectAttached);
    }
    void LateUpdate()
    {
        if (objectAttached)
        {
            movable_object.transform.position = hold_point.position;
        }
    }
    private void Attach()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, range_limit, movable))
        {
            movable_object = hit.transform.gameObject;

            movable_object_rb = movable_object.GetComponent<Rigidbody>();
            movable_object_cl = movable_object.GetComponent<Collider>();
            
            movable_object_rb.useGravity = false;
            movable_object_cl.isTrigger = true;

            hit.transform.position = hold_point.position;
            
            objectAttached = true;
        }
    }
    private void Detach()
    {
        movable_object_rb.useGravity = true;
        movable_object_cl.isTrigger = false;

        movable_object = null;
        movable_object_rb = null;

        objectAttached = false;
    }
}
