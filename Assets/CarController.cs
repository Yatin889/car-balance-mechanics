using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 1000f; 
    float movement;
    float rotationMov;
    public float AngularSpeed=50f;
    public float Brakemangnitude;

    bool onBackButtonPressed = false;

    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel;
    Rigidbody2D carRB;

    Vector2 pos;
    Vector2 posEnd;
    bool isTouchEnd=false;
    // Start is called before the first frame update

    void Start()
    {
        carRB=GetComponent<Rigidbody2D>();
        AngularSpeed = 60f;
        Brakemangnitude = 600f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // movement = Input.GetAxisRaw("Vertical") * speed;
        //rotationMov = Input.GetAxisRaw("Horizontal") * AngularSpeed;
        Translation();
        //Rotational();
        BrakeApply();
        
    }
    private void Update()
    {
        touchControl();
        Rotational();
    }

    private void touchControl()
    {
        if (Input.touchCount > 0f)
        {
            Debug.Log(Input.touchCount);

            Touch touch = Input.GetTouch(0);
            // if (touch.phase == TouchPhase.Moved)
            if (touch.phase == TouchPhase.Began)
            {
                pos = touch.position;

                // pos=Camera.main.ScreenToWorldPoint(touch.position);
                Debug.Log("touch Began");
                //transform.position = pos;
                //Debug.DrawLine(Vector2.zero, transform.position,Color.red );

            }
            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("touch ended");
                // Vector2 posEnd = Camera.main.WorldToScreenPoint(touch.position);
                posEnd = touch.position;
              //  transform.position = posEnd - pos;
                Debug.Log(posEnd.y-pos.y);
                isTouchEnd = true;

            }
        }
    }

    private void Rotational()
    {
        /* if (!(rotationMov == 0f))
         {
             carRB.AddTorque(-rotationMov);
         }*/
        float ytouchDisplacemnt;
        ytouchDisplacemnt = posEnd.y - pos.y;
       if(ytouchDisplacemnt>0f&&isTouchEnd)
        {
            
            rotationMov = -AngularSpeed;
            carRB.AddTorque(-rotationMov*20f);
            isTouchEnd = false;
        }
       else if(ytouchDisplacemnt<-200f&&isTouchEnd)
        {
            rotationMov = AngularSpeed;
            carRB.AddTorque(-rotationMov*20f);
            isTouchEnd = false;
        }
        else
        {
            rotationMov = 0;
            carRB.AddTorque(rotationMov);
        }
    }

    private void Translation()
    {
        if (movement == 0F)
        {
            frontWheel.useMotor = false;
            backWheel.useMotor = false;
           
        }
        else
        {
            Debug.Log("button working");
            frontWheel.useMotor = true;
            backWheel.useMotor = true;

            //for struct type
            JointMotor2D Bmotor = new JointMotor2D(); 
            Bmotor.motorSpeed = -movement;
            //Value=0 is there in maxTorgue can't just assign one value and move on you have to assign both or all values of struct type  
            Bmotor.maxMotorTorque = 10000F;
            frontWheel.motor = Bmotor;
          
            backWheel.motor = Bmotor;
        }
    }
    public void ButtonUP()
    {
        movement = 0F;
        Translation();
        onBackButtonPressed = false;
        carRB.angularDrag = 20f;
    } 
        
    public void ForwardButtonPressed()
    {
        movement = speed;
        Debug.Log("forward btn clicked");
        isTouchEnd = false;
        Translation();
    } 
    public void BackwardButtonPressed()
    {
        /*movement = -0.5f*speed;
        Debug.Log("backward btn clicked");
        Translation();
        */
        onBackButtonPressed = true;
    }

    private void BrakeApply()
    {
        if (onBackButtonPressed)
        {
            carRB.AddForce(Vector2.left * Brakemangnitude*Time.fixedDeltaTime, ForceMode2D.Force);
            carRB.angularDrag = 1000f;
            Debug.Log("pressed back");
        }
       
    }

    public void upRotationPressed()
    {
        rotationMov = -AngularSpeed;
        Rotational();
    }
    public void downRotationPressed()
    {
        rotationMov = AngularSpeed;
        Rotational();
    }
    public void RotationUpButton()
    {
        rotationMov = 0f;
        Rotational();
    }
}
