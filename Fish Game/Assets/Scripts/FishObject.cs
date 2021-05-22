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

    //keep track of what the fish is doing
    public enum FishState {Spawning, Swimming, Turning, Caught, Despawning };
    private FishState CurrentState;

    //reference to the sprite used when fish are still in the water
    //this sprite is made of multiple sprite components
    private SpriteRenderer[] ShadowSprites;

    //keep track of which way the fish is swimming
    private Vector3 direction;
    //keep track of how many times we've swam across the pond
    private int swimCounter;

    // Start is called before the first frame update
    void Start()
    {
        ShadowSprites = this.GetComponentsInChildren<SpriteRenderer>();
        CurrentState = FishState.Spawning;
        foreach (SpriteRenderer ShadowSprite in ShadowSprites)
        {
            ShadowSprite.color = new Color(ShadowSprite.color.r, ShadowSprite.color.g, ShadowSprite.color.b, 0); //start fish as transparent so they can fade in
        }
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
                break;
            case FishState.Caught:
                break;
            case FishState.Despawning:
                FadeOut();
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

        CaughtSprite = data._fishSprite;
        Name = data._name;
        Description = data._desc;
        SwimSpeed = data._swimSpeed;
        SwimDuration = data._swimDuration;
        Rarity = data._rarity;

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
            if (ShadowSprite.color.a >= 1)
            {
                if (SceneManager.Instance.ActiveFish.Contains(this.gameObject)) { SceneManager.Instance.ActiveFish.Remove(this.gameObject); }
                Object.Destroy(this);
            }
        }
    }

    //move forward at swim speed
    void Swim()
    {
        transform.Translate(direction * SwimSpeed * Time.deltaTime);
        //todo: when we reach a certain position (edge of water), increment swim counter, then either turn around or despawn
    }

    //true to set up the fish to swim left, false for right
    public void SetDirectionLeft(bool left)
    {
        //i dont like these weird hard coded rotations but unfortunately our sprites are not oriented the way unity wants (up vector is forward vector)
        Vector3 angles = new Vector3(90, 0, -90);
        if (left) { angles.z *= -1; }
        transform.rotation = Quaternion.Euler(angles);
        direction = transform.forward *-1;
    }
}
