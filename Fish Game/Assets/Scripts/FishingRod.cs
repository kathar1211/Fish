using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishingRodState { Ready, Casting, Fishing, Reeling}

public class FishingRod : MonoBehaviour
{
    public FishingRodState rodState;
    public GameObject bobber;
    public float planeX;
    public float minZ;
    public float maxZ;
    public float planeY;

    public GameObject castPoint;
    public GameObject rotationPoint;
    
    public SpriteRenderer castGauge;
    float castCharge = 0;
    float maxCastCharge = 50;
    float minCastCharge = 5;
    bool chargeUp = true;
    Vector3 castStart;
    Vector3 castEnd;
    float castIncrement = 0;

    float reelSpeed = 0.02f;
    public KeyCode reelButton;
    float reelIncrement;

    void Start() {
        castGauge.color = Color.white;
        reelButton = KeyCode.Q;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rodState);
        if(rodState == FishingRodState.Casting)
        {
            if(castIncrement < 1)
            {
                castIncrement += Time.deltaTime * 1f;
                bobber.transform.position = Vector3.Lerp(castStart, castEnd, castIncrement) + (Vector3.up * 10 * (1 - castIncrement));
            }
            else
            {
                rodState = FishingRodState.Fishing;
                castIncrement = 0;
            }
        }

        Debug.Log(reelButton + " : " + reelIncrement);
        reelIncrement += Time.deltaTime * 1f;
        if((int)reelIncrement%2 == 0) { reelButton = KeyCode.Q; }
        else { reelButton = KeyCode.E; }
        if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            Reel();
        }

        if(rodState != FishingRodState.Casting && Vector3.Distance(castPoint.transform.position,bobber.transform.position) < 1)
        {
            rodState = FishingRodState.Ready;
            if(Input.GetKey(KeyCode.Space))
            {
                ChargeCast();
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                CastOut();
            }
        }
    }

    void ChargeCast()
    {
        if(chargeUp)
        {
            castCharge += .02f;
            if(castCharge > maxCastCharge)
            {
                chargeUp = false;
            }
        }
        else
        {
            castCharge -= .02f;
            if(castCharge < minCastCharge)
            {
                chargeUp = true;
            }
        }

        Color col = new Color(castCharge/maxCastCharge,.5f,.5f);
        castGauge.color = col;
    }

    void CastOut()
    {
        castStart = bobber.transform.position;
        CastPosition();
        rodState = FishingRodState.Casting;
    }

    void CastPosition()
    {
        float powerPercentage = castCharge / maxCastCharge;
        float castX = planeX;
        float castY = planeY;
        float castZ = ((maxZ - minZ) * powerPercentage) - maxZ;
        castEnd = new Vector3(castX,castY,castZ);
    }

    void Reel()
    {
        bool correctKey = false;

        //Wiggle
        Vector3 rotationZ = rotationPoint.transform.localEulerAngles;
        if(Input.GetKey(KeyCode.Q) && reelButton == KeyCode.Q)
        {
            //newPos = Vector3.left;
            //Rotate Positive Z
            rotationZ = rotationPoint.transform.localEulerAngles + new Vector3(0f,0f,.05f);
            correctKey = true;
        }
        if(Input.GetKey(KeyCode.E) && reelButton == KeyCode.E)
        {
            //newPos = Vector3.right;
            //Rotate Negative Z
            rotationZ = rotationPoint.transform.localEulerAngles + new Vector3(0f,0f,-.05f);
            correctKey = true;
        }
        if(rotationZ.z > 2 && rotationZ.z < 300) {rotationZ.z = 2;}
        if(rotationZ.z < 358 && rotationZ.z > 60) {rotationZ.z = 358;}
        rotationPoint.transform.localEulerAngles = rotationZ;

        Vector3 currentBobberPos = bobber.transform.position;
        Vector3 castPos = castPoint.transform.position;

        Vector3 newPos = castPos - currentBobberPos;
        newPos.Normalize();

        newPos *= reelSpeed;
        if(currentBobberPos.z > minZ)
        {
            newPos.y = 0f;
        }

        if(correctKey) { bobber.transform.position += newPos; }
    }
}
