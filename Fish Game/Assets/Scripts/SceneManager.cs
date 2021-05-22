using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages objects in the scene, such as spawning and despawning fish
public class SceneManager : MonoBehaviour
{
    //let all other classes easily get a reference to the scenemanager (there should only be one)
    public static SceneManager Instance;

    //all the data for our different fish types
    public Fish[] FishTypes;
    //fish that have been spawned in the scene
    public List<GameObject> ActiveFish;
    //max number of fish active at a time
    public int MaxFish;
    //places to spawn a fish at - we need to know if it's on the left or right so we can set the fish's direction correctly
    public Vector3[] LeftSpawnLocations;
    public Vector3[] RightSpawnLocations;
    //basic template for all fish
    public GameObject FishPrefab;

    //time between spawning fish (in seconds)
    public float FishCooldown;
    //keep track of how long until we can spawn another fish (in seconds)
    private float FishTimer;

    //Awake is called before start
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveFish.Count < MaxFish)
        {
            SpawnFish();
        }
    }

    //create a fish and add it to the scene
    void SpawnFish()
    {
        //if we're not ready to spawn a fish just tick down the timer
        if (FishTimer > 0)
        {
            FishTimer -= Time.deltaTime;
            return;
        }

        //if we are ready to spawn a fish then reset the timer
        FishTimer = FishCooldown;


        //this could get inefficient (in theory we could be stuck in this loop for a while)
        //, but loop through grabbing random fish unti one is able to spawn
        bool readyToSpawn = false;
        Fish fishToSpawn = null;
        while (!readyToSpawn)
        {
            //grab a random fish type
            int fishIndex = Random.Range(0, FishTypes.Length);
            Fish fish = FishTypes[fishIndex];

            //fish will have a rarity 1 to 10 - translate this into a percent chance to spawn (1 is 100% 10 is 10%?)
            float percentChance = 10 + (100 - (fish._rarity * 10));
            percentChance /= 100.0f;

            //chance for spawn to succeed/fail
            float successChance = Random.Range(0.0f, 1.0f);
            if (successChance < percentChance)
            {
                fishToSpawn = fish;
                readyToSpawn = true;
            }
        }

        //once we have a fish type, create a fish prefab with appropriate values and spawn it in a spawn location
        GameObject newFish = GameObject.Instantiate(FishPrefab);
        newFish.GetComponent<FishObject>().SetupFish(fishToSpawn);
        //50/50 chance of spawning on the left or right
        bool spawnLeft = (Random.Range(0.0f, 1.0f) >= .5f);
        Vector3 spawnPos;
        if (spawnLeft)
        {
            spawnPos = LeftSpawnLocations[Random.Range(0, LeftSpawnLocations.Length)];
        }
        else
        {
            spawnPos = RightSpawnLocations[Random.Range(0, RightSpawnLocations.Length)];
        }
        newFish.transform.position = spawnPos;
        newFish.GetComponent<FishObject>().SetDirectionLeft(!spawnLeft); //if we spawned on left set it to face right and vice versa

        //add the new fish to our list of fish and we're done
        ActiveFish.Add(newFish);
    }
}
