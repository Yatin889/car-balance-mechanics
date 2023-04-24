using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 1000f; 
    float movement;
    float rotationMov;
    public float AngularSpeed=50f;
    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel;
    Rigidbody2D carRB;
    
    // Start is called before the first frame update
    void Start()
    {
        carRB=GetComponent<Rigidbody2D>();
        AngularSpeed = 40f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement = Input.GetAxisRaw("Vertical") * speed;
        rotationMov = Input.GetAxisRaw("Horizontal") * AngularSpeed;
        Translation();
        Rotational();
    }

    private void Rotational()
    {
        if (!(rotationMov == 0f))
        {
            carRB.AddTorque(-rotationMov);
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
}
