using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Rigidbody rb;
    public int speed;
    public float floorEdge = -10.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * -speed * Time.deltaTime);

        if(transform.position.z < floorEdge){
            Destroy(gameObject);
        }
    }
    
}
