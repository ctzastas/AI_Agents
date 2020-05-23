using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Player data
    [Range(5f,50f)]
    [SerializeField] private float speed;
    private Rigidbody player;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<Rigidbody>();
    }

    // We use FixedUpdate when we use forces
    void FixedUpdate() {
        movement = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));
        player.AddForce(speed * movement, ForceMode.Acceleration);
    }
}
