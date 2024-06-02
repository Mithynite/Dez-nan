using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensitivityX;
    public float sensitivityY;
    float rotationX;
    float rotationY;
    public Transform orientation;
    private Vector3 position;
    //private WallRun wallRunScript;
    [SerializeField] PlayerCam c;
    [SerializeField] GameObject player;
    void Awake()
    {
        //wallRunScript = player.GetComponent<WallRun>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
   
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX; 
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        rotationX -= mouseY;
        rotationY += mouseX;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);//otočení kamery
        orientation.rotation = Quaternion.Euler(0, rotationY, 0); //posun hráče po ose Y
        transform.position = orientation.position;
    }
}
