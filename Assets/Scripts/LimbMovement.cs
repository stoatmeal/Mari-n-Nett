using UnityEngine;
using UnityEngine.InputSystem;

public class LimbMovement : MonoBehaviour
{
    //evil code of evilness
    //This is actually bad I'm sorry for anyone who tries to read this or use it or play it


    //Input Keys
    [SerializeField] private InputAction leftArmInput; 
    [SerializeField] private InputAction rightArmInput;
    [SerializeField] private InputAction leftLegInput;
    [SerializeField] private InputAction rightLegInput;
    [SerializeField] private InputAction holdInput;

    //Bool that checks whether or not limbs are held in place
    public bool limbsHeld = false;


    public float rotationSpeed = 200;

    public GameObject leftArm;
    public GameObject rightArm;

    public GameObject leftLeg;
    public GameObject rightLeg;

    void Start()
    {
        leftArmInput.Enable();
        rightArmInput.Enable();
        leftLegInput.Enable();
        rightLegInput.Enable();
        holdInput.Enable();
        limbsHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleHold();
        SetArmRotation(leftArm, leftArmInput, 200, 350);

        SetArmRotation(rightArm,rightArmInput,160,10);
        SetArmRotation(leftLeg,leftLegInput,270,0);
        SetArmRotation(rightLeg,rightLegInput,90,0);
    }

//If Space Pressed, toggle limbs being held in place
public void ToggleHold(){
    if (holdInput.WasPressedThisFrame()){
        limbsHeld = !limbsHeld;
        Debug.Log("Limbs Pinned: "+limbsHeld);
    }
    
}

public void SetArmRotation(GameObject arm, InputAction inputButton, float topAngle, float bottomAngle)
    {
    //Z axis angle of arm in degrees, updated every frame
    float angle = arm.transform.localEulerAngles.z;
    float targetAngle;

    // wrap around handler, calculates shortest angle needed for rotation.
    angle = Mathf.DeltaAngle(0f, angle);

    if (inputButton.IsPressed())
    {
        // Rotate towards top angle
        targetAngle = topAngle;
    }
    else
    {
        // Return toward bottom angle
        targetAngle = bottomAngle;
    }
    //fun new if statement just dropped 
    //love this little guy 
    //When button pressed, swap direction
    float direction = inputButton.IsPressed() ? -1f : 1f;
    
    //rotate towards top or bottom angle depending on if button is pressed
    float newAngle = Mathf.MoveTowardsAngle(angle,targetAngle, rotationSpeed * Time.deltaTime);
    
    //move arm according to newAngle
    if(!limbsHeld){
        arm.transform.localRotation = Quaternion.Euler(0f, 0f, newAngle);}
    }
}
    