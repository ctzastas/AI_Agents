using UnityEngine;

public class Audio : MonoBehaviour {
    
    // Audio data
    private AudioSource audio;
    
    // Start is called before the first frame update
    void Start() {
        audio = GetComponent<AudioSource>();
        audio.Stop();
    }

    void OnTriggerEnter(Collider other) {
        // When a player or an enemy get on the plate play the sound
        if (other.gameObject.CompareTag("Player")  || other.gameObject.CompareTag("Enemy")) {
            audio.Play();
        }
    }
    
    // When a player or an enemy get away of plate stop the sound
    void OnTriggerExit(Collider other) {
        audio.Stop();
    }
}
