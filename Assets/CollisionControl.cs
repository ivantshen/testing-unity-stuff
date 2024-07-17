using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
Physics2D.IgnoreLayerCollision(6,8,true);
Physics2D.IgnoreLayerCollision(6,6,true);
Physics2D.IgnoreLayerCollision(3,3,true);
Physics2D.IgnoreLayerCollision(7,3,true);
Physics2D.IgnoreLayerCollision(7,7,true);
Physics2D.IgnoreLayerCollision(7,8,true);
Physics2D.IgnoreLayerCollision(8,6,true);
Physics2D.IgnoreLayerCollision(8,7,true);
Physics2D.IgnoreLayerCollision(8,8,true);
Physics2D.IgnoreLayerCollision(11,11,true);
Physics2D.IgnoreLayerCollision(11,7,true);
Physics2D.IgnoreLayerCollision(11,8,true);
Physics2D.IgnoreLayerCollision(12,0,true);
Physics2D.IgnoreLayerCollision(12,1,true);
Physics2D.IgnoreLayerCollision(12,2,true);
Physics2D.IgnoreLayerCollision(12,4,true);
Physics2D.IgnoreLayerCollision(12,5,true);
Physics2D.IgnoreLayerCollision(12,6,true);
Physics2D.IgnoreLayerCollision(12,7,true);
Physics2D.IgnoreLayerCollision(12,8,true);
Physics2D.IgnoreLayerCollision(12,9,true);
Physics2D.IgnoreLayerCollision(12,10,true);
Physics2D.IgnoreLayerCollision(12,11,true);
    }

}
