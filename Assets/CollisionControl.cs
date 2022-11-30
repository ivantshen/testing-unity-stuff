using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
Physics2D.IgnoreLayerCollision(6,8);
Physics2D.IgnoreLayerCollision(6,6);
Physics2D.IgnoreLayerCollision(7,3,true);
Physics2D.IgnoreLayerCollision(7,7,true);
Physics2D.IgnoreLayerCollision(7,8,true);
Physics2D.IgnoreLayerCollision(8,6,true);
Physics2D.IgnoreLayerCollision(8,7,true);
Physics2D.IgnoreLayerCollision(8,8,true);
Physics2D.IgnoreLayerCollision(11,11,true);
Physics2D.IgnoreLayerCollision(11,7,true);
Physics2D.IgnoreLayerCollision(11,8,true);
    }

}
