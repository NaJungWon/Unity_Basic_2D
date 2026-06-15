using UnityEngine;

namespace Study_ActionPlatformer
{
    public class PlayerStat : BaseStat
    {
        
    }

    [System.Serializable]
    public struct AttackInfo
    {
        public AttackKey key;
        public int MinDamage;
        public int MaxDamage;
        public AnimationCurve damageCurve;

        // 구조체(Struct)도 메서드를 가질 수 있다.
        // "이 데이터를 어떻게 해석(데미지 계산 공식)하는가?"에 대한 내용은
        // 데이터 곁에 두는것이 응집도가 좋습니다.
        // 기능이 많아지면 분리(확장함수)하는게 좋지만,
        // 몇개 없으면 struct 내부에다가 구현해놓고 사용하는거 ㄱㅊ

        public int RollDamage()
        {
            return 0;
        }
    }

    public enum AttackKey
    {
        None = 0,
        Combo1,
        Combo2,
        Combo3,
        JumpAttack,
    }
    
    // 스탯, 전투 관련 기능을 넣어놓을 겁니다. 그리고 어떤 개체에서든 Player를
    // 찾을 수 있는 기능을 만들겁니다.
    // CombatEntity를 상속 받았기 때문에 "전투에 참여하는 개체"의
    // 공통부(Picot, TakeDamage 등 순수가상함수 계약)는
    // 물려 받고, 플레이어의 고유의 것만 여기 남습니다.

    public class Player : CombatEntity
    {
        public static Player LocalPlayer { get; set; }

        public AttackInfo attackInfo;
        public override BaseStat BaseStat =>  Stat;
        private PlayerStat Stat { get; set; }


        public override void TakeDamage(int damage)
        {

        }

        public override void TakeHeal(int heal)
        {

        }
    }
}