using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerMovement : MonoBehaviour {

    //Player object speed
    [SerializeField]
    float speed=10;

    //player object's rigidbody component
    private Rigidbody rgd;

    //weapon to smack things with
    [SerializeField]
    GameObject stick;

    //weapon's rigidbody
    private Rigidbody rgdStick;

    //how far the weapon is from the front of the player
    [SerializeField]
    float frontOffset = 2;

    //how far to the side of the player
    [SerializeField]
    float sideOffset = .5f;

    [SerializeField]
    float cameraOffset = 10;

    [SerializeField]
    float cameraOffsetVertical = 10;

    //direction the player can face
    private enum Directions {LEFT,RIGHT,UP,DOWN};
    //current facing direction
    Directions currentDirection;

    //camera object
    [SerializeField]
    Camera camera;

	// Use this for initialization
	void Start () {
        //get the rigidbody component instance from object
        rgd = GetComponent<Rigidbody>();

        //get the rigidbody component instance from the weapon object
        rgdStick = stick.GetComponent<Rigidbody>();

        camera = Camera.main;
        camera.transform.position = transform.position+new Vector3(0,0,-cameraOffset);
	}

    private void FixedUpdate()
    {
        //get axis information dynamically as user input is collected
        float moveX = Input.GetAxis("SeekerMovementX");
        float moveY = Input.GetAxis("SeekerMovementY");

        //move the player object
        rgd.MovePosition(new Vector3(moveX, 0, moveY) * speed * Time.deltaTime + transform.position);

        //freeze the rotation of the weapon object
        rgdStick.freezeRotation = true;

        //set the facing direction of the seeker object
        SetDirection(moveX, moveY);

        MoveToDirection();

        //move the weapon with the player
        MoveStick();

        MoveCamera();
    }

    private void LateUpdate()
    {
        
    }

    //Move camera with player
    private void MoveCamera()
    {
        camera.transform.LookAt(this.transform);
        switch (currentDirection)
        {

            case Directions.UP:
                {

                    camera.transform.position = transform.position + new Vector3(0, cameraOffsetVertical, -cameraOffset);
                    break;

                }
            case Directions.DOWN:
                {

                    camera.transform.position = transform.position + new Vector3(0, cameraOffsetVertical, cameraOffset);
                    break;

                }
            case Directions.LEFT:
                {

                    camera.transform.position = transform.position + new Vector3(cameraOffset, cameraOffsetVertical, 0);
                    break;

                }
            case Directions.RIGHT:
                {

                    camera.transform.position = transform.position + new Vector3(-cameraOffset, cameraOffsetVertical, 0);
                    break;

                }
        }

    }

    //change the player object's facing direction
    private void SetDirection(float x,float y)
    {
        //left
        if(x<0)
        {
            currentDirection = Directions.LEFT;
        }
        //right
        if(x>0)
        {
            currentDirection = Directions.RIGHT;
        }
        //down
        if(y<0)
        {
            currentDirection = Directions.DOWN;
        }
        //up  
        if (y > 0)
        {
            currentDirection = Directions.UP;
        }

       

    }

    //move player more smoothly
    private void MoveToDirection()
    {
        switch (currentDirection)
        {

            case Directions.UP:
                {

                    rgd.rotation = Quaternion.Lerp(rgd.rotation, Quaternion.Euler(new Vector3(0, 360)), 0.1f);
                    break;

                }
            case Directions.DOWN:
                {

                    rgd.rotation = Quaternion.Lerp(rgd.rotation, Quaternion.Euler(new Vector3(0, 180)), 0.1f);
                    break;

                }
            case Directions.LEFT:
                {

                    rgd.rotation = Quaternion.Lerp(rgd.rotation, Quaternion.Euler(new Vector3(0, -90)), 0.1f);
                    break;

                }
            case Directions.RIGHT:
                {

                    rgd.rotation = Quaternion.Lerp(rgd.rotation, Quaternion.Euler(new Vector3(0, 90)), 0.1f);
                    break;

                }
        }
    }

    //move weapon with player object
    private void MoveStick()
    {
        //rotate to the left
        if (currentDirection.Equals(Directions.LEFT))
        {
            stick.transform.position = new Vector3(transform.position.x - frontOffset, transform.position.y, transform.position.z+sideOffset);
            rgdStick.rotation = Quaternion.Euler(new Vector3(90, -90));
        }
        //rotate to the right
        if (currentDirection.Equals(Directions.RIGHT))
        {
            stick.transform.position = new Vector3(transform.position.x + frontOffset, transform.position.y, transform.position.z+sideOffset);
            rgdStick.rotation = Quaternion.Euler(new Vector3(90, 90));
        }
        //rotate down
        if (currentDirection.Equals(Directions.DOWN))
        {
            stick.transform.position = new Vector3(transform.position.x+sideOffset, transform.position.y, transform.position.z - frontOffset);
            rgdStick.rotation = Quaternion.Euler(new Vector3(90, 180));
        }
        //rotate up
        if (currentDirection.Equals(Directions.UP))
        {
            stick.transform.position = new Vector3(transform.position.x+sideOffset, transform.position.y, transform.position.z + frontOffset);
            rgdStick.rotation = Quaternion.Euler(new Vector3(90, 360));
        }
    }

    //make the stick swing and hit like bat
    private void AttackWithStick()//TO-DO
    {
        float hit = Input.GetAxis("SeekerHit");
        if(hit!=0)
        {
            //animation and other needed code
        }
    }
}
