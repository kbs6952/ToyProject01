using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    PlayerMovementManager player;
    [Header("애니메이션 제어 변수")]
    private Animator animator;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void SetAttack()
    {
        player.GetComponent<PlayerMovementManager>();
        animator.SetTrigger("doAttack");
    }
    public void PlayerTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true,bool canRotate =false,bool canMove=false)       // 애니메이션 클립의 이름을 호출하여 각 playerManager에서 애니메이션을 쉽게 호출할 수 있게 캡슐화한 함수
    {
        animator.CrossFade(targetAnimation, 0.2f);
       
    }

    public void AnimatorTest()
    {
        // 애니메이션 공격 이펙트 실행

        Debug.Log("1111");
    }
}

