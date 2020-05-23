using UnityEngine;

public class Procedural : MonoBehaviour {

    public static Procedural instance;

    [SerializeField] GameObject grass;
    private int mapSizeX = 50;
    private int mapSizeZ = 50;

    // Use this for initialization
    void Start() {
        if (instance == null) {
            instance = this;
        }
            
        for (int x = 0; x < mapSizeX; x++) {
            for (int z = 0; z < mapSizeZ; z++) {
                float y = 0;
                GameObject generated;
                // if the height of the object is less than 1 generated in the scene
                if (y <= 1) {
                    generated = Instantiate(grass) as GameObject;
                    generated.transform.position = new Vector3(x, y, z);
                    generated.transform.SetParent(this.transform);
                }               
            }
        }
    }
}
