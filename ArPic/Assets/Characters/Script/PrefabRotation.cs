using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PrefabRotation : MonoBehaviour
{
    public MultiAimConstraint aimConst;

    void Update()
    {
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        if (transform.localPosition.z < 0)
        {
            aimConst.data.limits = new Vector2(-90f, 1f);
        }
        else
        {
            aimConst.data.limits = new Vector2(-90f, 90f);
        }
    }
}
