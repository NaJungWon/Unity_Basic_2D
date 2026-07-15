using UnityEngine;

namespace Study_OOP_And_Design_Pattern.Creational
{
    // Factory Method - 예시 A : 적 생성 (정석, 다형성 유지)
    // 상황 : 스포너 종류에 따라 다른 적을 만든다.
    // 형태 : 부모가 "만든다는 절차"만, 무엇을 만들지는 자식이 결정(가상 함수)

    public abstract class Monster : MonoBehaviour
    {
        public abstract string Name { get; }
        public float Speed { get; set; }
        public float Size { get; set; }
        
        public void Introduce()
        {
            Debug.Log($"[Factory A] {Name} 등장!");
        }
    }

    public class Goblin : Monster { public override string Name => "Goblin"; }

    public class Orc : Monster
    {
        public override string Name => "Orc";
        public string Equipment;
    }

    public abstract class MonsterSpanwer
    {
        protected abstract Monster CreateMonster();

        public Monster Spawn(Vector3 position)
        {
            Monster monster = CreateMonster();
            monster.transform.position = position;
            return monster;
        }
    }

    public class GoblinSpanwer : MonsterSpanwer
    {
        public float MinSpeed = 0;
        public float MaxSpeed = 5;
        
        protected override Monster CreateMonster()
        {
            GameObject gameObject = new GameObject("Goblin");
            Goblin monster = gameObject.AddComponent<Goblin>();
            monster.Speed = RollSpeed(MinSpeed, MaxSpeed);
            return monster;
        }

        private float RollSpeed(float min, float max)
        {
            return Random.Range(min, max);
        }
    }
    
    public class OrcSpanwer : MonsterSpanwer
    {
        public int MinSize = 1;
        public int MaxSize = 5;
        protected override Monster CreateMonster()
        {
            GameObject gameObject = new GameObject("Orc");
            Orc monster = gameObject.AddComponent<Orc>();
            monster.Size = RollSize(MinSize, MaxSize);
            monster.Equipment = RollEquipMent();
            return monster;
        }

        private float RollSize(int min, int max)
        {
            return Random.Range(min, max);
        }

        private string RollEquipMent()
        {
            string[] items = {"양손 도끼","양손 검", "양손 창" };
            int randIndex = Random.Range(0, items.Length);
            return items[randIndex];
        }
    }
    
    public class MonsterFactoryDemo : MonoBehaviour
    {
        private MonsterSpanwer monsterSpanwer;
        
        private void Start()
        {
            SelectMonsterSpawner();
            Monster monster = monsterSpanwer.Spawn(transform.position);
        }

        private void SelectMonsterSpawner()
        {
            int randNum = Random.Range(0, 1);

            switch (randNum)
            {
                case 0:
                    monsterSpanwer = new GoblinSpanwer();
                    break;
                case 1:
                    monsterSpanwer = new OrcSpanwer();
                    break;
            }
        }
    }
    
    //--------------- Troll 추가시 코드 ---------------

    public class Troll : Monster
    {
        public override string Name => "Troll";
        public string Equipment;
    }
    
    public class TrollSpawner : MonsterSpanwer
    {
        protected override Monster CreateMonster()
        {
            GameObject gameObject = new GameObject("Troll");
            Troll troll = gameObject.AddComponent<Troll>();
            troll.Equipment = "몽둥이";
            return troll;
        }
    }
}