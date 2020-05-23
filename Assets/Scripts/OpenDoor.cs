using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour {

    // Door animation data
    private Animator animator;
    // Text data
    [SerializeField] private TextMeshProUGUI openDoorText;
    [SerializeField] private GameObject openDoorPanel;
    
    // Awake is used to initialize any variables or game state before the game starts
    void Awake() {
        openDoorPanel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }
    
    // When the player reach the door push space key button to open it
    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            openDoorPanel.SetActive(true);
            openDoorText.text = "Press space to open the door";
        }
        if (Input.GetKey(KeyCode.Space)) {
            animator.SetBool("Open",true);         
        }
    }
    
    // When the player go away from the door this closed
    void OnCollisionExit(Collision collision) {
        animator.SetBool("Open",false);
        openDoorPanel.SetActive(false); 
    }
    
    // When the player or an enemy reach the door this opens automatically
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player")) {
            animator.SetBool("Open",true);
        }
    }
    
    // When the player or an enemy go away from the door this closed
    void OnTriggerExit(Collider other) {
        animator.SetBool("Open",false);
    }
}
