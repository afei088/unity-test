using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            jumpKeyPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    // Fixed Update is called once very physics update
    private void FixedUpdate() 
    {   
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyPressed) 
        {
            rigidbodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer == 6) 
        {
            Destroy(other.gameObject);
        }
    }
}
