using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float floorEdge = -10.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        floorEdge = -10.2f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * -speed * Time.deltaTime, ForceMode.Impulse);

        if(transform.position.z < floorEdge){
            Destroy(gameObject);
        }
    }
    public void SetSpeed(float setSpeed){
        speed = setSpeed; 
    }

}
