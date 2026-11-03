using UnityEngine;

public class TurnOnGravity : MonoBehaviour
{

    private Rigidbody rb;
    private GameObject floor;
    private float floorEdge = -10.2f;
    public bool gravityOn = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        floor = GameObject.FindWithTag("Floor");

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < floorEdge){
            rb.useGravity = true;
            gravityOn = true;
            Debug.Log("Drop Gravity");

        }
    }
}
