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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            CastPosition();
        }

        if(Input.GetMouseButton(0))
        {
            Reel();
        }

        if(Vector3.Distance(castPoint.transform.position,bobber.transform.position) < 1)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                CastOut();
            }
        }
    }

    void CastOut()
    {
        bobber.transform.position += Vector3.forward * (Random.Range(6,18));
    }

    void CastPosition()
    {
        Vector2 screenPos = Input.mousePosition;
        float widthPercentage = screenPos.x / Screen.width;
        float heightPercentage  = screenPos.y / Screen.height;

        float castX = ((maxX - minX) * widthPercentage) - maxX;
        float castY = planeY;
        float castZ = ((maxZ - minZ) * heightPercentage) - maxZ;

        Vector3 currentCastPointPos = castPoint.transform.position;
        Vector3 castPos = new Vector3(castX,castY,castZ);

        Vector3 newPos = castPos - currentCastPointPos;
        newPos *= .005f;
        newPos.y = 0f;

        castPoint.transform.position += newPos;
    }

    void Reel()
    {
        Vector3 currentBobberPos = bobber.transform.position;
        Vector3 castPos = castPoint.transform.position;

        Vector3 newPos = castPos - currentBobberPos;
        newPos *= .005f;
        newPos.y = 0f;

        bobber.transform.position += newPos;
    }
}
