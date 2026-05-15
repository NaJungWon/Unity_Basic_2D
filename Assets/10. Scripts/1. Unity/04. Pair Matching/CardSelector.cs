using UnityEngine;
using UnityEngine.InputSystem;


namespace Study.ParMatchingGame
{
    public class CardSelector : MonoBehaviour
    {
        public enum Direction
        {
            Up, Down, Left, Right
        }
        [Header("Ref Object")]
        public UnityEngine.Transform cursor;

        [Header("Settings")]
        public float cursorYOffset = -0.5f;

        private Card[] cards; //외부에서(PairMatchingGame)에서 주입받습니다.
        public int currentIndex = 2;
        

        private Card selectCardA;
        private Card selectCardB;

        //이번 프레임에 카드 선택이 완료되었는지 체크하는 bool 변수
        public bool wasSelectionCompleted = false;

        private void Update()
        {
            wasSelectionCompleted = false;

            if(Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                MoveCursor(Direction.Left);
            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                MoveCursor(Direction.Right);
            }
            else if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                MoveCursor(Direction.Up);
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                MoveCursor(Direction.Down);
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                SelectCard();
            }

            if (Keyboard.current.pKey.wasPressedThisFrame)
            {
                Debug.Log($"A : {selectCardA}, B : {selectCardB}");
            }
            if (Keyboard.current.oKey.wasPressedThisFrame)
            {
                Debug.Log($"A : {selectCardA}, B : {selectCardB}");
            }
        }

        private void SelectCard()
        {
            Card currentCard = cards[currentIndex];

            if (selectCardA == null)
            {
                selectCardA = currentCard;
            }
            //같은 카드를 선택되지 않게 합시다.
            else if (selectCardA == currentCard)
            {
                return;
            }
            else
            {
                selectCardB = currentCard;
                wasSelectionCompleted = true;
            }

            cards[currentIndex].Flip();
        }

        public void SetBoard(Card[] cardArray)
        {
            cards = cardArray;
        }

        /// <summary>
        /// 0번은 첫번째로 선택한 카드, 1번은 두번째로 선택한 카드
        /// </summary>
        /// <returns></returns>
        public Card[] GetSelectedCards()
        {
            return new[] { selectCardA, selectCardB };
        }

        public void Clear()
        {
            selectCardA = null;
            selectCardB = null;
        }

        // cards배열에 존재하지 않는 개체를 건너뛰는 기능을 만들어야 합니다.
        // currentIndex를 적용하기 전에 해당 배열이 null인지 체크해보면 됩니다.
        private void MoveCursor(bool isLeft)
        {

            int temp = currentIndex;

            for (int i = 0; i < cards.Length; ++i)
            {
                temp += isLeft ? -1 : +1;

                if (temp < 0) temp = cards.Length - 1;
                if (temp >= cards.Length) temp = 0;

                // cards[temp]가 null이라면
                if (cards[temp] == null)
                {
                    currentIndex++;
                    continue;
                }
                else
                {
                    // 커서 움직이게 하고 종료
                    currentIndex = temp;
                    float cardX = cards[currentIndex].transform.position.x;
                    float cardY = cards[currentIndex].transform.position.y + cursorYOffset;
                    cursor.position = new Vector3(cardX, cardY);
                    return;
                }
            }

            // 카드가 아무것도 없는 상태
            currentIndex = -1;
        }

        // 함수 오버로딩
        // -같은 이름의 함수더라도, 사용하는 매개변수가 다르다면
        // 중복 정의가 가능합니다.
        // (반환자료형-함수이름-매개변수)=> 함수의 시그니쳐라고 보통 말합니다
        private void MoveCursor(Direction direction)
        {
            const int COLUMN_COUNT = 5;

            switch(direction)
            {
                case Direction.Up:
                    for (int i = 0; i < COLUMN_COUNT; i++)
                    {
                        MoveCursor(false);
                    }
                    break;
                case Direction.Down:
                    for (int i = 0; i < COLUMN_COUNT; i++)
                    {
                        MoveCursor(true);
                    }
                    break;
                case Direction.Left:
                    MoveCursor(true);
                    break;
                case Direction.Right:
                    MoveCursor(false);
                    break;
            }
        }
    }
}


