using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour, IEventReciver<eCharacterHello>
{
    [SerializeField] Animator anim;

    Transform target;

    bool flagRotate = false;
    bool rightRotate;

    bool flag = false;

    public void OnEvent(eCharacterHello @event)
    {
        //anim.SetTrigger("hello");
    }

    private void Start()
    {
        anim.SetTrigger("hello");
    }

    void Update()
    {
        if (flag && IsLookingAtTarget())
        {
            flag = false;
            flagRotate = true;
            anim.SetTrigger("hello");
            if (rightRotate)
            {
                anim.SetBool("right", true);
            }
            else
            {
                anim.SetBool("left", true);
            }
        }
        if (flagRotate && IsLookingAtTargetStart())
        {
            flagRotate = false;
            if (rightRotate)
            {
                anim.SetBool("right", false);
                anim.SetBool("rightEnd", true);
            }
            else
            {
                anim.SetBool("left", false);
                anim.SetBool("leftEnd", true);
            }
        }
    }

    void OnTriggerEnter (Collider other)
    {
        anim.SetBool("right", false);
        anim.SetBool("rightEnd", false);
        anim.SetBool("left", false);
        anim.SetBool("leftEnd", false);

        target = other.gameObject.transform;

        if (IsToTheRight())
        {
            anim.SetTrigger("rightStart");
            rightRotate = true;
        }
        else
        {
            anim.SetTrigger("leftStart");
            rightRotate = false;
        }
        flagRotate = false;
        flag = true;
    }

    bool IsLookingAtTargetStart()
    {
        float rot = transform.localEulerAngles.y;
        return rot < 5f;
    }

    bool IsLookingAtTarget()
    {
        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0;

        Vector3 forward = transform.forward;

        float angle = Vector3.Angle(forward, directionToTarget);

        return angle < 5f;
    }

    bool IsToTheRight()
    {
        Vector3 directionToTarget = target.position - transform.position;

        Vector3 rightDirection = transform.right;

        float crossProduct = Vector3.Cross(rightDirection, directionToTarget).y;

        return crossProduct > 0;
    }

    virtual protected void OnEnable()
    {
        EventBus.Register(this as IEventReciver<eCharacterHello>);
    }

    virtual protected void OnDisable()
    {
        EventBus.Unregister(this as IEventReciver<eCharacterHello>);
    }
}
