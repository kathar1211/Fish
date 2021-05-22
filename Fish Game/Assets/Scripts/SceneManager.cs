using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages objects in the scene, such as spawning and despawning fish
public class SceneManager : MonoBehaviour
{
    //all the data for our different fish types
    public Fish[] FishTypes;
    //fish that have been spawned in the scene
    public List<GameObject> ActiveFish;
    //max number of fish active at a time
    public int MaxFish;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
