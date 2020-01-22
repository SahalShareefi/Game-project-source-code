using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    //movement/panning
    public float MovSpeed = 25f;
	public float BorderThickness = 50f; // number of pixels <more less  >
    public float X_axisCamPositionOffset = -94f;// to determine the cam beginning pos
    public float Z_axisCamPositionOffset = 13f;// to determine the cam beginning pos
    public Vector3 DesiredPos;
    public Vector2 screenLimit;
    public Transform ObjTransform;
  //  Rect leftRect; 
   // Rect rightRect;yy
    //Rect upRect;
   // Rect downRect;
    // ----------------
    public KeyCode panningKey = KeyCode.Mouse2;
    public float panningSpeed = 100f;

    // zoom
    public float zomSpeed=30f;
	public float minY = -8f;
	public float maxY = 30f;

    // Rotation
   // public KeyCode mouseRotationKey = KeyCode.Mouse1;
    public float mouseOrbitingSpeed = 77f;

   // Vector3 camDistance;
  //  Vector3 hitRayPosition;
    Transform hitRayTransform;
    // bool lookAtOrbitPos = true;

    private void Start()
    {
        ObjTransform.position = this.transform.position;
      


        //  leftRect = new Rect(0, 0, BorderThickness, Screen.height);
        //  rightRect = new Rect(Screen.width - BorderThickness, 0, BorderThickness, Screen.height);
        //   upRect = new Rect(0, Screen.height - BorderThickness, Screen.width, BorderThickness);
        // downRect = new Rect(0, 0, Screen.width, BorderThickness);
    }

    void Update () {

      //  Rotation();

        zooming();

        movement();

      //  Rotation();
        // movement/zoom limits
        ObjTransform.position = new Vector3(Mathf.Clamp(ObjTransform.position.x, -screenLimit.x+ X_axisCamPositionOffset, screenLimit.x + X_axisCamPositionOffset), Mathf.Clamp(ObjTransform.position.y, minY, maxY), Mathf.Clamp(ObjTransform.position.z, -screenLimit.y + Z_axisCamPositionOffset, screenLimit.y + Z_axisCamPositionOffset));

    }

    public void movement()
    {
       
        if (Input.GetKey(panningKey) == false && Input.GetKey(KeyCode.Mouse1) == false)
        {
      
            if (Input.mousePosition.y>=Screen.height - BorderThickness)// here another technique/*upRect.Contains(MouseInput)
            {
                DesiredPos.z += 1;
            }
            if (Input.mousePosition.y <= BorderThickness)//down
            {
                DesiredPos.z -= 1;
            }
            if (Input.mousePosition.x >= Screen.width - BorderThickness)//right
            {
                DesiredPos.x += 1;
            }
            if (Input.mousePosition.x <= BorderThickness)//left
            {
                DesiredPos.x -= 1;
            }

            DesiredPos *= MovSpeed * Time.deltaTime;
            DesiredPos = transform.InverseTransformDirection(DesiredPos);
            ObjTransform.Translate(DesiredPos, Space.Self);
        }
        // panning 
        if (Input.GetKey(panningKey) == true)
        {

            Vector3 desiredPanningMove = new Vector3(-MouseAxis.x, 0, -MouseAxis.y);

            desiredPanningMove *= panningSpeed * Time.deltaTime;
            desiredPanningMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredPanningMove;
            desiredPanningMove = ObjTransform.InverseTransformDirection(desiredPanningMove);
            ObjTransform.Translate(desiredPanningMove, Space.Self);
        }

       
    }
    // rotation
    private void Rotation()
    {
        if (Input.GetKey(KeyCode.Mouse1)==true )
        {
            OrbitPointPosition();

            ObjTransform.RotateAround(hitRayTransform.position, new Vector3(0, ObjTransform.position.y, 0), MouseAxis.x* mouseOrbitingSpeed * Time.deltaTime);
        }
    }

    void OrbitPointPosition()
    {
        //point to orbit around
       if(Input.GetKeyDown(KeyCode.Mouse1)==false){ //if( lookAtOrbitPos == true) { 
          Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
          RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //hitRayPosition = hit.point;
                hitRayTransform = hit.transform;
            }
          //  lookAtOrbitPos = false;
        }

    }
    public void zooming()
    {
        //to slow cam movement when it is zoomed
        if (ObjTransform.position.y <= 10 && ObjTransform.position.y >= 3)
        {
            panningSpeed = 50;
            MovSpeed = 15;
        }
        else if (ObjTransform.position.y <= 3 && ObjTransform.position.y >= -8)
        {
            panningSpeed = 28;
            MovSpeed = 7;
        }
        else
        {
            panningSpeed = 100;
            MovSpeed = 25;
        }
    

        if (Input.GetKeyDown(KeyCode.Mouse1) == false) {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            DesiredPos.y -= scroll * zomSpeed * 100f * Time.deltaTime;
        }
    }

    private Vector2 MouseAxis
    {
        get { return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); }
    }

    private Vector2 MouseInput
    {
        get { return Input.mousePosition; }
    }
/*
    private void OnMouseEnter()
    {

        Vector2 current_mouse_postion;
        current_mouse_postion = Input.mousePosition;
    }
    */
}
