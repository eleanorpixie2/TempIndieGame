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

    [SerializeField]
    GameObject prefab;

    [SerializeField]
    GameObject target;

    //weapon's rigidbody
    private Rigidbody rgdStick;

    //how far the weapon is from the front of the player
    [SerializeField]
    float frontOffset = 2;

    //how far to the side of the player
    [SerializeField]
    float sideOffset = .5f;

    [SerializeField]
    Vector3 cameraOffset;

    [SerializeField]
    float cameraOffsetVertical = 10;

    [SerializeField]
    Canvas can;

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
        //
        camera = Camera.main;
        rgd= this.GetComponent<Rigidbody>();

        //
        camera.transform.position = this.transform.position + new Vector3(0, 0, cameraOffsetVertical * -1);
        cameraOffset = camera.transform.position + this.transform.position;
    }

    private void FixedUpdate()
    {
        //get axis information dynamically as user input is collected
        float moveX = Input.GetAxis("SeekerMovementX");
        float moveY = Input.GetAxis("SeekerMovementY");

        //move camera 
        //camera.transform.position += (new Vector3(moveX, 0, moveY) * speed * Time.deltaTime);
        //move the player object
       // rgd.MovePosition(camera.transform.rotation * new Vector3(-moveX, 0, -moveY) * speed * Time.deltaTime + transform.position);
        rgd.freezeRotation=true;

        //freeze the rotation of the weapon object
        rgdStick.freezeRotation = true;

        //set the facing direction of the seeker object
        SetDirection(moveX, moveY);

        AttackWithStick();

        MoveToDirection();

        //move the weapon with the player
        MoveStick();

        MoveCamera();
    }

    //Move camera with player
    private void MoveCamera()
    {

        //
        float mouseX = Input.GetAxis("Horizontal");
        float mouseY = -Input.GetAxis("Horizontal");

        //if (Input.mousePosition.x < Screen.width ||
        //    Input.mousePosition.x > 0 ||
        //    Input.mousePosition.y < Screen.height ||
        //    Input.mousePosition.y > 0)
        //{

        //    Cursor.lockState = CursorLockMode.Locked;

        //}

        if (mouseX != 0 || mouseY != 0)
        {

            //
            Quaternion xTurnAngle = new Quaternion();
            Quaternion yTurnAngle = new Quaternion();

            xTurnAngle = Quaternion.AngleAxis(mouseX, new Vector3(0, 5, 0));
            yTurnAngle = Quaternion.AngleAxis(mouseY, new Vector3(0, -5, 0));
            cameraOffset = xTurnAngle * yTurnAngle * cameraOffset;

        }

        Vector3 newCameraPosition = this.transform.position + cameraOffset.normalized * cameraOffsetVertical;
        newCameraPosition.y += 8.0f;

        //
        camera.transform.position = Vector3.Slerp(camera.transform.position, newCameraPosition, 0.1f);
        camera.transform.LookAt(this.transform);


    }

    //change the player object's facing direction
    private void SetDirection(float x,float y)
    {
        //left
        if(x < 0)
        {
            currentDirection = Directions.LEFT;
        }
        //right
        if(x > 0)
        {
            currentDirection = Directions.RIGHT;
        }
        //down
        if(y < 0)
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
        Vector3 ignorePitchAndRoll = camera.transform.rotation.eulerAngles;
        ignorePitchAndRoll.x = 0;
        ignorePitchAndRoll.z = 0;

        //
        Quaternion cameraRotation = new Quaternion();
        cameraRotation.eulerAngles = ignorePitchAndRoll;

        Vector3 original = new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);

        //get axis information dynamically as user input is collected
        float moveX = Input.GetAxis("SeekerMovementX");
        float moveY = Input.GetAxis("SeekerMovementY");

        switch (currentDirection)
        {
            case Directions.LEFT:
                rgd.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                this.transform.position += (cameraRotation * Vector3.left * moveX * -1) * speed * Time.deltaTime;

                //
                ignorePitchAndRoll.y -= 90;
                cameraRotation.eulerAngles = ignorePitchAndRoll;

                //
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, cameraRotation, 0.1f);
                break;
            case Directions.RIGHT:
                rgd.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                this.transform.position += (cameraRotation * Vector3.right * moveX) * speed * Time.deltaTime;

                //
                ignorePitchAndRoll.y += 90;
                cameraRotation.eulerAngles = ignorePitchAndRoll;

                //
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, cameraRotation, 0.1f);
                break;
            case Directions.UP:
               // rgd.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                this.transform.position += (cameraRotation * Vector3.forward * -moveY) * speed * Time.deltaTime;
                //
                ignorePitchAndRoll.y -= 180;
                cameraRotation.eulerAngles = ignorePitchAndRoll;

                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, cameraRotation, 0.1f);
                break;
            case Directions.DOWN:
               // rgd.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
                this.transform.position += (cameraRotation * Vector3.back * moveY) * speed * Time.deltaTime;



                //
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, cameraRotation, 0.1f);
                break;
            default:
                break;
        }
    }

    //move weapon with player object
    private void MoveStick()
    {
        ////rotate to the left
        //if (currentDirection.Equals(Directions.LEFT))
        //{
        //    stick.transform.position = new Vector3(transform.position.x - frontOffset, transform.position.y + frontOffset, transform.position.z+sideOffset);
        //    rgdStick.rotation = Quaternion.Euler(new Vector3(90, -90));
        //}
        ////rotate to the right
        //if (currentDirection.Equals(Directions.RIGHT))
        //{
        //    stick.transform.position = new Vector3(transform.position.x + frontOffset, transform.position.y + frontOffset, transform.position.z+sideOffset);
        //    rgdStick.rotation = Quaternion.Euler(new Vector3(90, 90));
        //}
        ////rotate down
        //if (currentDirection.Equals(Directions.DOWN))
        //{
        //    stick.transform.position = new Vector3(transform.position.x+sideOffset, transform.position.y + frontOffset, transform.position.z - frontOffset);
        //    rgdStick.rotation = Quaternion.Euler(new Vector3(90, 180));
        //}
        ////rotate up
        //if (currentDirection.Equals(Directions.UP))
        //{
        //    stick.transform.position = new Vector3(transform.position.x+sideOffset, transform.position.y + frontOffset, transform.position.z + frontOffset);
        //    rgdStick.rotation = Quaternion.Euler(new Vector3(90, 360));
        //}

        Vector3 stickRotation = this.transform.rotation.eulerAngles;
        stickRotation.x = 90;

        Quaternion newStickRotation = Quaternion.Euler(stickRotation);

        if(currentDirection.Equals(Directions.LEFT))
        {

            Vector3 newStickPosition = this.transform.position + this.transform.rotation * new Vector3(0, 0, 1) * frontOffset;
            stick.transform.position = newStickPosition;
            rgdStick.rotation = newStickRotation;

        }
        if (currentDirection.Equals(Directions.RIGHT))
        {

            Vector3 newStickPosition = this.transform.position + this.transform.rotation * new Vector3(0, 0, -1) * frontOffset * -1;
            stick.transform.position = newStickPosition;
            rgdStick.rotation = newStickRotation;

        }
        if (currentDirection.Equals(Directions.UP))
        {

            Vector3 newStickPosition = this.transform.position + new Vector3(frontOffset * -1, 0, 0);
            stick.transform.position = newStickPosition;
            rgdStick.rotation = newStickRotation;

        }
        if (currentDirection.Equals(Directions.DOWN))
        {

            Vector3 newStickPosition = this.transform.position + new Vector3(frontOffset, 0, 0);
            stick.transform.position = newStickPosition;
            rgdStick.rotation = newStickRotation;

        }

    }

    //make the stick swing and hit like bat
    private void AttackWithStick()//TO-DO
    {
        float hit = Input.GetAxis("SeekerHit");

        if (hit != 0 && GameObject.FindGameObjectsWithTag("bullet").Length<1)
        {
            //create bullet object
            GameObject bullet = Instantiate(
                               prefab,
                               stick.transform.position,
                               stick.transform.rotation);

            Debug.Log("TEST" + this.transform.rotation * new Vector3(-1, 0));

            switch (currentDirection)
            {
                //shoot out based on facing direction
                case Directions.UP:
                    {
                        bullet.GetComponent<Rigidbody>().velocity += this.transform.rotation * new Vector3(0, 0, 10);
                        break;

                    }
                case Directions.DOWN:
                    {
                        bullet.GetComponent<Rigidbody>().velocity += this.transform.rotation * new Vector3(0, 0, 10);
                        break;

                    }
                case Directions.LEFT:
                    {

                        bullet.GetComponent<Rigidbody>().velocity += this.transform.rotation * new Vector3(0, 0, 10);
                        break;

                    }
                case Directions.RIGHT:
                    {

                        bullet.GetComponent<Rigidbody>().velocity += this.transform.rotation * new Vector3(0, 0, 10);
                        break;

                    }
            }


            // Destroy the bullet after 2 seconds
            Destroy(bullet, 2.0f);
        }
    }
}
