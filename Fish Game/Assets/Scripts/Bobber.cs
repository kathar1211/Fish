using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{

    public GameObject bobber;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float planeY;

    public GameObject castPoint;
    public GameObject rotationPoint;
    
    public SpriteRenderer castGauge;
    float castCharge = 6;
    bool chargeUp = true;

    float reelSpeed = 0.01f;

    void Start() {
        castGauge.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        CastPosition();

        if(Input.GetKeyDown(KeyCode.W))
        {
            reelSpeed += 0.001f;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            reelSpeed -= 0.001f;
        }

        if(Input.GetMouseButton(0))
        {
            Reel();
        }

        if(Vector3.Distance(castPoint.transform.position,bobber.transform.position) < 1)
        {
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
            castCharge += .01f;
            if(castCharge > 18)
            {
                chargeUp = false;
            }
        }
        else
        {
            castCharge -= .01f;
            if(castCharge < 6)
            {
                chargeUp = true;
            }
        }

        Color col = new Color(castCharge/18,.5f,.5f);
        castGauge.color = col;
    }

    void CastOut()
    {
        Vector3 cast = Vector3.forward * castCharge;
        cast.y = bobber.transform.position.y * -1;
        bobber.transform.position += cast;
    }

    void CastPosition()
    {

        /* Vector2 screenPos = Input.mousePosition;
        float widthPercentage = screenPos.x / Screen.width;
        float heightPercentage  = screenPos.y / Screen.height;
        float castX = ((maxX - minX) * widthPercentage) - maxX;
        float castY = planeY;
        float castZ = ((maxZ - minZ) * heightPercentage) - maxZ;
        Vector3 currentCastPointPos = castPoint.transform.position;
        Vector3 castPos = new Vector3(castX,castY,castZ);
        Vector3 newPos = castPos - currentCastPointPos;
        newPos *= .005f;
        newPos.y = 0f; */

        //Vector3 currentCastPos = castPoint.transform.position;
        //Vector3 newPos = Vector3.zero;
        Vector3 rotationZ = rotationPoint.transform.localEulerAngles;
        if(Input.GetKey(KeyCode.Q))
        {
            //newPos = Vector3.left;
            //Rotate Positive Z
            rotationZ = rotationPoint.transform.localEulerAngles + new Vector3(0f,0f,.05f);
        }
        if(Input.GetKey(KeyCode.E))
        {
            //newPos = Vector3.right;
            //Rotate Negative Z
            rotationZ = rotationPoint.transform.localEulerAngles + new Vector3(0f,0f,-.05f);
        }
        if(rotationZ.z > 20 && rotationZ.z < 300) {rotationZ.z = 20;}
        if(rotationZ.z < 340 && rotationZ.z > 60) {rotationZ.z = 340;}
        rotationPoint.transform.localEulerAngles = rotationZ;

        //newPos *= .01f;
        //newPos.y = 0f;

        //castPoint.transform.position += newPos;
    }

    void Reel()
    {
        Vector3 currentBobberPos = bobber.transform.position;
        Vector3 castPos = castPoint.transform.position;

        Vector3 newPos = castPos - currentBobberPos;
        newPos.Normalize();



        reelSpeed = Mathf.Clamp(reelSpeed, .001f, .02f);
        newPos *= reelSpeed;
        //newPos.y = 0f;

        bobber.transform.position += newPos;
    }
}
