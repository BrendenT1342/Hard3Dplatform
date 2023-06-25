using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Transform playerTransform;
    public float groundCheckRadius;
    public float groundCheckDistance;
    private bool isGrounded;
    private Rigidbody rb;
    
    public Vector3 _input;
    public Vector3 offset;
    public Camera cam;
    [SerializeField] AudioSource source;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.playing)
        {
            rb.isKinematic = false;
            return;
        }
        // Move character using the AWSD.
        _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        

        // The jump indicates the space button to jump.
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            source.Play();
        }

        //Camera
        _input = cam.transform.TransformDirection(_input);
        _input.y = 0f;
        CustomGravity();
        
    }

    private void FixedUpdate()
    {
        // Identifies the player is on ground.
        isGrounded = CheckGrounded();
        if (_input.magnitude > 1)
        {
            _input.Normalize();
        }
        Vector3 movement = _input.normalized * (moveSpeed * Time.deltaTime);
        rb.velocity = movement;
        
        RotateToCameraDirection();
        
    }
    #region
    //Adjust gravity for the player.
    void CustomGravity()
    {
        rb.AddForce(Vector3.down * 90, ForceMode.Acceleration);
    }

    bool CheckGrounded()
    {
        RaycastHit info;
        if (Physics.SphereCast(transform.position + offset, groundCheckRadius, Vector3.down, out info, groundCheckDistance))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + offset, groundCheckRadius);
        Gizmos.DrawSphere(transform.position + offset + (Vector3.down * groundCheckDistance), groundCheckRadius);
    }

    void RotateToCameraDirection()
    {
        Vector3 flattenedCameraForward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up);
    }
    
    // If the player touches the item, it triggers the object to dissapear (Destroy the object).
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gold")
        {
            GameManager.score += 1;
            Destroy(other.gameObject);
        }
    }
    #endregion
}
