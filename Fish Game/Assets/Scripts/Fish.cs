using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic fish functionality, rarity, sprite, etc.
/// </summary>

[CreateAssetMenu(fileName ="New Fish", menuName = "Fish")]
public class Fish : ScriptableObject
{
    public Sprite _fishSprite;
    public string _name;
    //rarity will go from 1-10. 1 is least rare, 10 is most rare. figured this is easiest since we have 22 fish
    [Range(1,10)]
    public int _rarity;
    public string _desc;
    //how fast do fish swim in the water
    public float _swimSpeed;
    //how many back and forths does this fish do before it despawns
    public int _swimDuration;
    [Range(1,3)]
    public int _starCount;
}




