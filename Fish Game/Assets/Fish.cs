using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic fish functionality, rarity, sprite, etc.
/// </summary>

public class Fish : MonoBehaviour
{
    public Sprite _fishSprite;
    [SerializeField]
    private string _name;
    //rarity will go from 1-10. 1 is least rare, 10 is most rare. figured this is easiest since we have 22 fish
    [SerializeField]
    private int _rarity;
    [SerializeField]
    private string _desc;

    protected void setName()
    {

    }
    protected void setRarity()
    {

    }
    protected void setDescription()
    {

    }
}




