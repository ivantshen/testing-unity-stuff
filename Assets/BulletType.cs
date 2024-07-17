using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletType : MonoBehaviour
{
    [SerializeField] private int bulletType;
    public int getBulletType(){
        return bulletType;
    }
}
