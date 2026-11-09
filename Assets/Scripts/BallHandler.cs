using UnityEngine;

public class BallHandler : MonoBehaviour
{
    public bool IsTouched;

    public Material Off;
    public Material On; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay(Collider other){
        IsTouched = true;
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null && On != null)
        {
            renderer.material = On;
        }
    }
    private void OnTriggerExit(Collider other){
        IsTouched = false;
        if (GetComponent<Renderer>() != null && Off != null)
        {
            GetComponent<Renderer>().material = Off;
        }
    }
    
}
