using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attached to fish gameobjects, manages behaviors in the water
public class FishObject : MonoBehaviour
{
    //attributes that vary by type of fish
    private Sprite CaughtSprite;
    private string Name;
    private string Description;
    private float SwimSpeed;
    private int SwimDuration;
    [Range(1, 10)]
    private int Rarity;
    private Fish FishData;

    //keep track of what the fish is doing
    public enum FishState {Spawning, Swimming, Turning, Caught, Escaped, Despawning, PreBiting, Biting };
    private FishState CurrentState;

    //reference to the sprite used when fish are still in the water
    //this sprite is made of multiple sprite components
    private SpriteRenderer[] ShadowSprites;

    //keep track of which way the fish is swimming
    private Vector3 direction;
    //keep track of how many times we've swam across the pond
    private int swimCounter;

    //minimum amount of time (in seconds) the fish will consider biting 
    public float MinNibbleTime;
    //maximum amount of time (in seconds) the fish will consider biting
    public float MaxNibbleTime;
    //how long does the player have to start reeling in the fish before it gets away (also in seconds)
    public float BiteWindow;

    //how long will the fish actually consider biting;
    private float NibbleTime;

    //used to keep track of the passge of time when fish has started nibbling or biting
    private float timer;

    //need reference to the bobber to attach self
    private GameObject bobber;
    //and fishing rod to check state
    private FishingRod fishingRod;

    // Start is called before the first frame update
    void Start()
    {
        ShadowSprites = this.GetComponentsInChildren<SpriteRenderer>();
        CurrentState = FishState.Spawning;
        foreach (SpriteRenderer ShadowSprite in ShadowSprites)
        {
            ShadowSprite.color = new Color(ShadowSprite.color.r, ShadowSprite.color.g, ShadowSprite.color.b, 0); //start fish as transparent so they can fade in
        }

        fishingRod = SceneManager.Instance.fishingRod;
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case FishState.Spawning:
                FadeIn();
                break;
            case FishState.Swimming:
                Swim();
                break;
            case FishState.Turning:
                TurnAround();
                break;
            case FishState.Caught:
                Reeling();
                break;
            case FishState.Despawning:
                FadeOut();
                break;
            case FishState.Escaped:
                SwimAndFadeOut();
                break;
            case FishState.PreBiting:
                ThinkingAboutBiting();
                break;
            case FishState.Biting:
                Biting();
                break;
            default:
                break;
        }
    }

    //assign this fish values from the fish template
    public void SetupFish(Fish data)
    {
        //this shouldnt happen but just in case
        if (data == null) { return; }

        FishData = data;
        CaughtSprite = data._fishSprite;
        Name = data._name;
        Description = data._desc;
        SwimSpeed = data._swimSpeed;
        SwimDuration = data._swimDuration;
        Rarity = data._rarity;
        BiteWindow = data._biteWindow;
        MinNibbleTime = data._minNibbleTime;
        MaxNibbleTime = data._maxNibbleTime;

        SetStartState();
    }

    //this wasnt working in start so we're calling it after setup now
    private void SetStartState()
    {
        ShadowSprites = this.GetComponentsInChildren<SpriteRenderer>();
        CurrentState = FishState.Spawning;
        foreach (SpriteRenderer ShadowSprite in ShadowSprites)
        {
            ShadowSprite.color = new Color(ShadowSprite.color.r, ShadowSprite.color.g, ShadowSprite.color.b, 0); //start fish as transparent so they can fade in
        }
    }

    //when fish is first spawned gradually appear
    void FadeIn()
    {
        foreach (SpriteRenderer ShadowSprite in ShadowSprites)
        {
            //increase the alpha slightly each frame
            ShadowSprite.color = new Color(ShadowSprite.color.r, ShadowSprite.color.g, ShadowSprite.color.b, ShadowSprite.color.a + .01f);
            //once we're at full opacity start swimming
            if (ShadowSprite.color.a >= 1)
            {
                CurrentState = FishState.Swimming;
            }
        }
    }

    //make an exit when fish's time on earth is up
    void FadeOut()
    {
        foreach (SpriteRenderer ShadowSprite in ShadowSprites)
        {
            //decrease the alpha slightly each frame
            ShadowSprite.color = new Color(ShadowSprite.color.r, ShadowSprite.color.g, ShadowSprite.color.b, ShadowSprite.color.a - .01f);
            //once we're at full transparency we're gone
            if (ShadowSprite.color.a <= 0)
            {
                if (SceneManager.Instance.ActiveFish.Contains(this.gameObject)) { SceneManager.Instance.ActiveFish.Remove(this.gameObject); }
                Object.Destroy(this.gameObject);
            }
        }
    }

    //move forward at swim speed
    void Swim()
    {
        transform.Translate(direction * SwimSpeed * Time.deltaTime, Space.World);
        //todo: when we reach a certain position (edge of water), increment swim counter, then either turn around or despawn
    }

    //true to set up the fish to swim left, false for right
    public void SetDirectionLeft(bool left)
    {
        //i dont like these weird hard coded rotations but unfortunately our sprites are not oriented the way unity wants (up vector is forward vector)
        Vector3 angles = new Vector3(-90, 0, -90);
        if (left) { angles.z *= -1; }
        transform.rotation = Quaternion.Euler(angles);

        if (left) { direction = Vector3.left; }
        if (!left) { direction = Vector3.right; }
    }

    //edge of the water has trigger colliders to notify fish when they're approaching the edge
    private void OnTriggerEnter(Collider other)
    {
        //if we've reached a boundary, reverse desired direction and start turning
        if (other.tag == "WaterBoundaries" && CurrentState == FishState.Swimming)
        {
            //increment how many times we've crossed the water and check if we've reached the limit
            swimCounter++;
            if (swimCounter >= SwimDuration)
            {
                CurrentState = FishState.Despawning;
                return;
            }

            direction *= -1;
            CurrentState = FishState.Turning;
        }

        //when we hit the bobber, start the pre biting phase
        else if (other.tag == "Bobber" && CurrentState == FishState.Swimming)
        {
            //the fish will "think" about biting for some amount of time between min and max nibble time before biting
            //set that time here
            NibbleTime = Random.Range(MinNibbleTime, MaxNibbleTime);
            //reset the timer, enter the nibble phase, and wait
            timer = 0;
            CurrentState = FishState.PreBiting;
            //todo: trigger the appropriate bobber animation here?
            Debug.Log(Name + " is thinking about biting");
            

            bobber = other.gameObject;
            bobber.GetComponent<Bobber>().PlayNibbled();
        }
    }

    //turn until direction lines up
    void TurnAround()
    {
        //rotate about world up axis as a function of rotate speed
        //transform.RotateAround(transform.position, Vector3.up, SwimSpeed * Time.deltaTime * 100);
        transform.Rotate(new Vector3(0,0, SwimSpeed * Time.deltaTime * 100));

        //if we're very close to matching the target direction, snap to position and keep swimming
        //because of wacky rotations we're using our up vector as a forward vector
        //the dot product of our "forward" vector and our target direction will give us the cos of the angle between them
        //if the angle is 0 degrees, the dot product will be 1. if the dot product is .9, the angle is pretty close to 0 degrees
        if (Vector3.Dot(direction, transform.up) >= .99)
        {
            //transform.up = direction;
            //transform.forward = Vector3.up;
            transform.rotation = Quaternion.LookRotation(Vector3.up, direction);
            CurrentState = FishState.Swimming;            
        }

    }

    void SwimAndFadeOut()
    {
        //if the fish got away, it swims forward while fading to transparent
        Swim();
        FadeOut();
        //this maybe didnt need to be its own method huh
    }

    //before the fish bites, it hovers in front of the bobber for some amount of time
    void ThinkingAboutBiting()
    {
        //increment timer
        timer += Time.deltaTime;

        //check if it's time to bite
        if (timer >= NibbleTime)
        {
            timer = 0;
            CurrentState = FishState.Biting;
            //todo: update bobber animation here?
            Debug.Log(Name + " is biting");
            bobber.GetComponent<Bobber>().PlayGrabbed();
        }
    }

    //bite the bobber for an amount of time, then either get caught or get away
    void Biting()
    {
        //increment timer
        timer += Time.deltaTime;

        //check for input 
        if (fishingRod.rodState == FishingRodState.Fishing && Input.GetKeyDown(fishingRod.reelButton))
        {
            CurrentState = FishState.Caught;
            Debug.Log("Caught " + name);
            SceneManager.Instance.catalog.FishCaught(FishData);
            if (SceneManager.Instance.ActiveFish.Contains(this.gameObject)) { SceneManager.Instance.ActiveFish.Remove(this.gameObject); }
            Object.Destroy(this.gameObject);
            fishingRod.Reset();
            //snap the fish to the bobber so it moves in with it
            //transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward * -1);
            //transform.parent = bobber.transform;
            //we'll be generous and reset the timer too
            //timer = 0;
        }

        //check if we've gotten away yet
        if (timer >= BiteWindow)
        {
            CurrentState = FishState.Escaped;
            Debug.Log(Name + " got away...");
            bobber.GetComponent<Bobber>().PlayIdle();
        }
    }

    //when fish is being reeled in, continue to countdown the bite timer but dont check for input anymore
    void Reeling()
    {
        //increment timer
        timer += Time.deltaTime;

        //check if we've gotten away yet
        if (timer >= BiteWindow)
        {
            /*CurrentState = FishState.Escaped;
            transform.parent = null;
            transform.rotation = Quaternion.LookRotation(Vector3.up, direction);
            Debug.Log(Name + " got away...");*/
        }

        //check if the player finished reeling in the fish
        if (fishingRod.rodState == FishingRodState.Ready)
        {
            Debug.Log("Caught " + name);
            SceneManager.Instance.catalog.FishCaught(FishData);
            if (SceneManager.Instance.ActiveFish.Contains(this.gameObject)) { SceneManager.Instance.ActiveFish.Remove(this.gameObject); }
            Object.Destroy(this.gameObject);
        }
    }

}
