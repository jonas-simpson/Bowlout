using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawn : MonoBehaviour
{
    public GameObject obj;
    public int num;
    public float force;
    public Vector3 dir;

    public void Spawn(Vector3 _direction, float _force)
    {
        GameObject myObject = Instantiate(obj, transform.position +
                              new Vector3(Random.Range(-1f,1f),0,0),
                              Quaternion.identity);
        BillboardSprite sprite = myObject.GetComponent<BillboardSprite>();
        sprite.ApplyForce(_direction, _force);
    }

}
