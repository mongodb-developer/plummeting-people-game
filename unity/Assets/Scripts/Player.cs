using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 1.5f;

    private Rigidbody2D rigidBody2D;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        movement = new Vector2(h * speed, v * speed);
        
    }

    void FixedUpdate()
    {
        rigidBody2D.velocity = movement;
        // if(Input.GetKey("w")) {
        //     rigidBody2D.AddForce(Vector3.up * speed);
        // }
        // if(Input.GetKey("s")) {
        //     rigidBody2D.AddForce(Vector3.down * speed);
        // }
        // if(Input.GetKey("a")) {
        //     rigidBody2D.AddForce(Vector3.left * speed);
        // }
        // if(Input.GetKey("d")) {
        //     rigidBody2D.AddForce(Vector3.right * speed);
        // }
    }

}