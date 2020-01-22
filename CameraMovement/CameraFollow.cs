using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour  {
	
	public GameObject access; // to use GetComponent Method in unity ( u can use just by GameObject variable type)
	private CameraController Accessor; // to access variables from class "CameraController" + u have to adjust it and put the object of acript "CameraController" into the currentBuilding script
	public Vector3 DistanceBetweenObjAndCamera;
	public float smoothSpeed = 0.16f; // if u do not use Time.deltaTime 1 - 0,,, 1 will move during 1 frame to desired position(like a cut) and 0.5 value between them(smoothy).


   /* // Rotation
    public bool useMouseRotation = true;
    public KeyCode mouseRotationKey = KeyCode.Mouse1;
    public KeyCode rotateRightKey = KeyCode.X;
    public KeyCode rotateLeftKey = KeyCode.Z;
    public float mouseRotationSpeed = 10f; */
    //mouse or touch



    void Start () {
		Accessor = access.GetComponent<CameraController> ();// to get all "CameraController" values.
		DistanceBetweenObjAndCamera = new Vector3(0,10f,0);
        transform.position =  Accessor.ObjTransform.position ;

    }

    private void Update()
    {
        // to rotate camera
        // Rotation();
        transform.position = Vector3.Lerp(transform.position, Accessor.ObjTransform.position + DistanceBetweenObjAndCamera, smoothSpeed);//(currentBuilding position, desired position, smoothSpeed)
        transform.rotation = Quaternion.Euler(55, Accessor.ObjTransform.transform.rotation.eulerAngles.y, 0); // camera angle
    }
    void FixedUpdate () {
        // smooth the camera following.

            


    }

}
