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
    [SerializeField] GameObject player;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
   
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX; // TODO Citlivost myši na ose X 
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY; // TODO Citlivost myši na ose Y

        rotationX -= mouseY;
        rotationY += mouseX;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // TODO Omezení pohledu Hráče, aby nemohl "přetočit kameru"

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0); // TODO Otočení kamery
        orientation.rotation = Quaternion.Euler(0, rotationY, 0); // TODO Posun hráče po ose Y
        transform.position = orientation.position;
    }
}
