using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThomasEdisonPlayerMove : MonoBehaviour {
    private CharacterController charController;
    private ThomasEdisonAnimation edisonAnim;
    public float movementSpeed;
    public float rotationSpeed;
    public float rotationPerDegree;
    public Joystick movementJoystick;
    private Vector3 moveDirection;
    private Vector3 rotateDirection;

    // Use this for initialization
	void Awake () {
        charController = GetComponent<CharacterController>();
        edisonAnim = GetComponent<ThomasEdisonAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
        move();
        rotate();
	}

    private void move()
    {

        moveDirection.y -= CONST.GRAVITY * Time.deltaTime;
        if (movementJoystick.Vertical > 0.5)
        {
            moveDirection = transform.forward;
            charController.Move(moveDirection*movementSpeed*Time.deltaTime);
            edisonAnim.walk(true);
        }

        else if (movementJoystick.Vertical < -0.5)
        {
            moveDirection = -transform.forward;
            charController.Move(moveDirection * movementSpeed * Time.deltaTime);
            edisonAnim.walk(true);
        }
        else
            edisonAnim.walk(false);
    }

    private void rotate()
    {
        rotateDirection = Vector3.zero;
        if (movementJoystick.Horizontal < -0.5)
        {
            rotateDirection = transform.TransformDirection(Vector3.left);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, Quaternion.LookRotation(rotateDirection),
                rotationPerDegree * Time.deltaTime * rotationSpeed);
        }

        else if(movementJoystick.Horizontal>0.5)
        {
            rotateDirection = transform.TransformDirection(Vector3.right);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, Quaternion.LookRotation(rotateDirection),
                rotationPerDegree * Time.deltaTime * rotationSpeed);
        }
    }

    
}

























