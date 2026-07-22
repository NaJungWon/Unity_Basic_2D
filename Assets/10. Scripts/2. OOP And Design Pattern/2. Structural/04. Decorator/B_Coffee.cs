using UnityEngine;

namespace Study_OOP_And_Design_Pattern.Structural.Decorator
{
    // Decorator - 예시 B : 커피 토핑
    // 상황 : 기본 커피에 우유, 샷, 시럽을 넣으면 가격/설명이 누적된다
    // 포인트 : 스탯과는 완전히 다른 도메인이면서 "감싸면서 누적"하는 구조의 반복
    
    // 커피에 우유를 추가할수있고, 샷을 추가하면서 설명과 가격이 증가하는
    // 구조를 만들어 봅시다.

    public interface ICoffee
    {
        int Price();
        string Description();
    }

    public class Espresso : ICoffee
    {
        public int Price() => 3000;
        public string Description() => "에스프레소";
    }

    public abstract class CoffeeDeco : ICoffee
    {
        protected ICoffee inner;
        protected CoffeeDeco(ICoffee inner)
        {
            this.inner = inner;
        }
        public abstract int Price();
        public abstract string Description();
    }

    public class Milk : CoffeeDeco
    {
        public Milk(ICoffee inner) : base(inner) { }
        public override int Price() => inner.Price() + 500;
        public override string Description() => inner.Description() + "+ 우유";
    }

    public class Shot : CoffeeDeco
    {
        public Shot(ICoffee inner) : base(inner) { }
        public override int Price() => inner.Price() + 300;
        public override string Description() => inner.Description() + "+ 샷 추가";
    }

    public class Ice : CoffeeDeco
    {
        public Ice(ICoffee inner) : base(inner) { }
        public override int Price() => inner.Price() + 100;
        public override string Description() => inner.Description() + "+ 얼음 추가";

    }

    public class CoffeeDemo : MonoBehaviour
    {
        public void Start()
        {
            Espresso espresso = new Espresso();
            espresso.Price();
            espresso.Description();
            
            Debug.Log($"[Coffee] 가격 : {espresso.Price()}, {espresso.Description()}");
        }
    }
}