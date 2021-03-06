using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages animations for the bobber
public class Bobber : MonoBehaviour
{

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayIdle()
    {
        animator.Play("Bobber_Idle",-1,0f);
    }

    public void PlayLand()
    {
        animator.Play("Bobber_Land",-1,0f);
    }

    public void PlayNibbled()
    {
        animator.Play("Bobber_Nibbled",-1,0f);
    }

    public void PlayGrabbed()
    {
        animator.Play("Bobber_Grabbed",-1,0f);
    }
}
