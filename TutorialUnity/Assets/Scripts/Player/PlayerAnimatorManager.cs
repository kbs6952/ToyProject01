using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    PlayerMovementManager player;
    [Header("�ִϸ��̼� ���� ����")]
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
    public void PlayerTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true,bool canRotate =false,bool canMove=false)       // �ִϸ��̼� Ŭ���� �̸��� ȣ���Ͽ� �� playerManager���� �ִϸ��̼��� ���� ȣ���� �� �ְ� ĸ��ȭ�� �Լ�
    {
        animator.CrossFade(targetAnimation, 0.2f);
       
    }

    public void AnimatorTest()
    {
        // �ִϸ��̼� ���� ����Ʈ ����

        Debug.Log("1111");
    }
}

