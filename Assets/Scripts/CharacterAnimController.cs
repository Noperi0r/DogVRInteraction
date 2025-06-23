using Oculus.Interaction;
using Oculus.Platform;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    [Header("Animation States (Inspector Editable)")]
    //[SerializeField] private bool isIdle;
    [SerializeField] private bool isWalk;
    [SerializeField] private bool isAction1;
    [SerializeField] private bool isAction2;

    [Header("Animator")]
    [SerializeField] private Animator _anim;

    [Space]
    [Header("Character Walk Properties")]
    [SerializeField] Transform playerTransform;
    float stopDistance = 3.3f;
    float rotationSpeed = 3f;

    float distPlayer2Chara;
    bool canWalkByDist;

    // 캐시된 이전 값들
    //private bool prevIdle;
    private bool prevWalk;
    private bool prevAction1;
    private bool prevAction2;

    void Update()
    {
        if (_anim == null) return;

        // if (prevIdle != isIdle)
        // {
        //     _anim.SetBool("isIdle", isIdle);
        //     prevIdle = isIdle;
        // }

        distPlayer2Chara = Vector3.Distance(transform.position, playerTransform.position);
        canWalkByDist = distPlayer2Chara > stopDistance;
        Debug.Log(distPlayer2Chara);

        if (!canWalkByDist) _anim.SetBool("isWalk", false);

        if (canWalkByDist && _anim.GetBool("isWalk"))
            RotateTowardsPlayer();

        if (prevWalk != isWalk)
        {
            _anim.SetBool("isWalk", isWalk);
            prevWalk = isWalk;
        }

        if (prevAction1 != isAction1)
        {
            _anim.SetBool("isAction1", isAction1);
            prevAction1 = isAction1;
        }

        if (prevAction2 != isAction2)
        {
            _anim.SetBool("isAction2", isAction2);
            prevAction2 = isAction2;
        }
    }

    void ResetStates()
    {
        //_anim.SetBool("isIdle", false);
        _anim.SetBool("isWalk", false);
        _anim.SetBool("isAction1", false);
        _anim.SetBool("isAction2", false);

        //isIdle = prevIdle = false;
        isWalk = prevWalk = false;
        isAction1 = prevAction1 = false;
        isAction2 = prevAction2 = false;
    }

    // public void EnableIdle()
    // {
    //     ResetStates();

    //     _anim.SetBool("isIdle", true);
    //     //isIdle = prevIdle = true;

    //     Debug.Log("[EnableIdle] + ", isWalk: " + isWalk + ", isAction1: " + isAction1 + ", isAction2: " + isAction2);
    // }

    // public void DisableIdle()
    // {
    //     ResetStates();

    //     _anim.SetBool("isIdle", false);
    //     isIdle = prevIdle = false;

    //     Debug.Log("[DisableIdle] isIdle: " + isIdle + ", isWalk: " + isWalk + ", isAction1: " + isAction1 + ", isAction2: " + isAction2);
    // }

    public void EnableWalk()
    {
        ResetStates();

        _anim.SetBool("isWalk", true);
        isWalk = prevWalk = true;
    }

    public void DisableWalk()
    {
        ResetStates();

        _anim.SetBool("isWalk", false);
        isWalk = prevWalk = false;
    }

    public void EnableAction1()
    {
        ResetStates();

        _anim.SetBool("isAction1", true);
        isAction1 = prevAction1 = true;
    }

    public void DisableAction1()
    {
        ResetStates();

        _anim.SetBool("isAction1", false);
        isAction1 = prevAction1 = false;

    }

    public void EnableAction2()
    {
        ResetStates();

        _anim.SetBool("isAction2", true);
        isAction2 = prevAction2 = true;
    }

    public void DisableAction2()
    {
        ResetStates();

        _anim.SetBool("isAction2", false);
        isAction2 = prevAction2 = false;
    }

    void RotateTowardsPlayer()
    {
        Vector3 dir = playerTransform.position - transform.position;
        dir.y = 0;
        if (dir.sqrMagnitude < 0.001f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }

    // void OnAnimatorMove()
    // {
    //     if (_anim.GetBool("isWalk"))
    //     {
    //         Vector3 motion = _anim.deltaPosition;
    //         transform.position += motion;
    //     }
    // }

}
