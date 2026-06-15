using UnityEngine;

namespace Study_ActionPlatformer
{
    // JumpStatesms Animator의 AnyState(모든 상태) 기능 활용하여 구현합니다.

    // # AnyState(모든 상태)?
    // - 항상 표시(출력, 재생)가능한 특수한 상태입니다.
    // 현재 존재하는 상태와 상관없이 특정 상태로 이동하려는 상황에 사용합니다.
    // - 상태머신(Animator)에 특수한 전환을 제어하는 방법입니다.
    // ex) Idle, Run 등의 지속적으로 Loop되는 애니메이션에는 사용하지 않습니다.
    public class JumpState : PlayerAnimStateBase
    {
        public JumpState(PlayerController owner) : base(owner) { }

        public override void Enter()
        {
            
        }

        public override void UpdateState(AnimatorStateInfo stateInfo)
        {
            // 상태 초반부에는 직전 공격 입력을 제거해준다
            if (stateInfo.normalizedTime < INPUT_RESET_TIME)
            {
                Animator.SetBool(PlayerController.IS_ATTACK, false);
            }

            // 점프 상태에서도 입력을 허용해준다.
            Owner.HandleMovement();
        }
    }
}
