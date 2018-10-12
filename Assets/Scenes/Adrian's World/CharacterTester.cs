using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTester : MonoBehaviour
{

    //
    [SerializeField]
    KeyCode forwardKey;
    [SerializeField]
    KeyCode backwardKey;
    [SerializeField]
    KeyCode leftKey;
    [SerializeField]
    KeyCode rightKey;
    [SerializeField]
    KeyCode interactKey;

    //
    [SerializeField]
    float cameraDistance;

    [SerializeField]
    float characterSpeed;

    //
    Camera thisCamera;
    Vector3 cameraOffset;
    Rigidbody thisRigidbody;

    //
    Animator doorAnimator;

    // Use this for initialization
    void Start ()
    {
        SetupCharacter();

	}

    void SetupCharacter()
    {

        //
        thisCamera = Camera.main;
        thisRigidbody = this.GetComponent<Rigidbody>();

        //
        thisCamera.transform.position = this.transform.position + new Vector3(0, 0, cameraDistance * -1);
        cameraOffset = thisCamera.transform.position + this.transform.position;

    }
	
	// Update is called once per frame
	void Update ()
    {

        InputHandler();
        AdjustCamera();

	}

    void InputHandler()
    {

        //
        Vector3 ignorePitchAndRoll = thisCamera.transform.rotation.eulerAngles;
        ignorePitchAndRoll.x = 0;
        ignorePitchAndRoll.z = 0;

        //
        Quaternion cameraRotation = new Quaternion();
        cameraRotation.eulerAngles = ignorePitchAndRoll;

        if (Input.GetKey(forwardKey))
        {

            this.transform.position += (cameraRotation * Vector3.forward) * characterSpeed * Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, cameraRotation, 0.1f);

        }
        if (Input.GetKey(backwardKey))
        {

            this.transform.position += (cameraRotation * Vector3.back) * characterSpeed * Time.deltaTime;

            //
            ignorePitchAndRoll.y -= 180;
            cameraRotation.eulerAngles = ignorePitchAndRoll;

            //
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, cameraRotation, 0.1f);

        }
        if (Input.GetKey(leftKey))
        {

            this.transform.position += (cameraRotation * Vector3.left) * characterSpeed * Time.deltaTime;

            //
            ignorePitchAndRoll.y -= 90;
            cameraRotation.eulerAngles = ignorePitchAndRoll;

            //
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, cameraRotation, 0.1f);

        }
        if (Input.GetKey(rightKey))
        {

            this.transform.position += (cameraRotation * Vector3.right) * characterSpeed * Time.deltaTime;

            //
            ignorePitchAndRoll.y += 90;
            cameraRotation.eulerAngles = ignorePitchAndRoll;

            //
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, cameraRotation, 0.1f);

        }
        if (Input.GetKeyDown(interactKey))
        {

            //
            Ray checkerRay = new Ray(this.transform.position, (this.transform.position + cameraOffset * -1).normalized);
            RaycastHit interactCheck;

            //
            Debug.DrawLine(this.transform.position, this.transform.position + cameraOffset * -1, new Color(0, 0, 1));
            if (Physics.Raycast(checkerRay, out interactCheck, 10.0f))
            {

                
                Debug.Log(interactCheck.collider.name);
                if (interactCheck.collider.tag == "Door")
                {

                    doorAnimator = interactCheck.collider.transform.parent.gameObject.GetComponent<Animator>();

                    if (doorAnimator.GetFloat("Blend") == 0)
                    {

                        doorAnimator.SetFloat("Blend", 2);

                    }
                    else if (doorAnimator.GetFloat("Blend") == 2)
                    {

                        doorAnimator.SetFloat("Blend", 0);

                    }

                }

            }

        }

        Blend();

    }

    void AdjustCamera()
    {

        //
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        if (Input.mousePosition.x < Screen.width ||
            Input.mousePosition.x > 0 ||
            Input.mousePosition.y < Screen.height ||
            Input.mousePosition.y > 0)
        {

            Cursor.lockState = CursorLockMode.Locked;

        }

        if (mouseX != 0 || mouseY != 0)
        {

            //
            Quaternion xTurnAngle = new Quaternion();
            Quaternion yTurnAngle = new Quaternion();

            xTurnAngle = Quaternion.AngleAxis(mouseX, new Vector3(0, 1, 0));
            yTurnAngle = Quaternion.AngleAxis(mouseY, new Vector3(-1, 0, 0));
            cameraOffset = xTurnAngle * yTurnAngle * cameraOffset;

        }

        Vector3 newCameraPosition = this.transform.position + cameraOffset.normalized * cameraDistance;

        //
        thisCamera.transform.position = Vector3.Slerp(thisCamera.transform.position, newCameraPosition, 0.1f);
        thisCamera.transform.LookAt(this.transform);

    }
    
    public void Blend()
    {

        if (doorAnimator != null)
        {

            float Henlo = doorAnimator.GetFloat("Blend");

        }

    }
}
