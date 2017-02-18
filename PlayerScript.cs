using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    Camera theCamera;
    Rigidbody rb;
    private Vector3 playerPosition;
    private float rotationY;
    public float lookYSpeed;
    public float lookXSpeed;
    public float minRotY;
    public float maxRotY;    
    public float moveSpeed;
    public float jumpPower;
    public float airControl;
    public float gravity;
    private float bounceTimer = 30.0f;
    private bool isBouncing = false;

    public bool applyGravity = true;

    public ParticleSystem splash1;
    public ParticleSystem splash2;

    public GameObject floor;

    // Use this for initialization
    void Start () {
        theCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        if(bounceTimer > 0)
        {
            bounceTimer--;
        } else
        {
            isBouncing = false;
        }

        playerPosition = transform.localPosition;

        // Move the player position forwards
        playerPosition += transform.forward * moveSpeed * Time.deltaTime;

        // Camera Controls
        float rotationX = Input.GetAxis("Mouse X") * lookXSpeed * Time.deltaTime;
        transform.Rotate(0, rotationX, 0);
        rotationY += Input.GetAxis("Mouse Y") * lookYSpeed * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, minRotY, maxRotY); // Clamping camera Y axis
        Vector3 lookVector = theCamera.transform.localEulerAngles;
        lookVector.x = -rotationY;
        theCamera.transform.localEulerAngles = lookVector;

        // Move the player rigidbody forwards
        //rb.MovePosition(playerPosition);

        if(rb.transform.position.y <= -1)
        {
            Bounce(Vector3.up, 15);
            //splash1.Play();
            //splash2.Play();
            Instantiate(splash2, transform.position, Quaternion.Euler(-90,0,0));
            //Instantiate(splash1, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.Euler(-90, 0, 0));
        }

    }

    public void Bounce(Vector3 bounceDirection, float bouncePower)
    {
        if (!isBouncing)
        {          
             
            //rb.AddForce(bounceDirection * bouncePower, ForceMode.Impulse);
            bounceTimer = 30f;
            isBouncing = true;
        }
            

    }

    void OnTriggerEnter(Collider other)
    {
        Bounce(Vector3.up, 25);
    }
}
