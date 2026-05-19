using UnityEngine;
using UnityEngine.InputSystem;


namespace Study.ParMatchingGame
{
    public class CardSelector2D : MonoBehaviour
    {
        public enum Direction
        {
            Up, Down, Left, Right
        }
        [Header("Ref Object")]
        public UnityEngine.Transform cursor;

        [Header("Settings")]
        public float cursorYOffset = -0.5f;

        private Card[ , ] cards;
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

        private int currentX = 2;
        private int currentY = 0;

        private void SelectCard()
        {
            Card currentCard = cards[currentX, currentY];

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

            cards[currentX, currentY].Flip();
        }

        public void SetBoard(Card[ , ] cardArray)
        {
            cards = cardArray;
        }

        public Card[] GetSelectedCards()
        {
            return new[] { selectCardA, selectCardB };
        }

        public void Clear()
        {
            selectCardA = null;
            selectCardB = null;
        }

        void UpgradeCursorPosition(int x, int y)
        {
            float cardX = cards[x,y].transform.position.x;
            float cardY = cards[x,y].transform.position.y + cursorYOffset;
            cursor.position = new Vector3(cardX, cardY);
        }

        private void MoveCursor(Direction dir)
        {
            switch(dir)
            {
                case Direction.Up:
                    currentY++;
                    break;
                case Direction.Down:
                    currentY--;
                    break;
                case Direction.Left:
                    currentX--;
                    break;
                case Direction.Right:
                    currentX++;
                    break;
            }
            if (currentY >= cards.GetLength(1)) currentY = 0; //행의 최대값을 벗어나면 0번 위치로
            else if (currentY < 0) currentY = cards.GetLength(1) - 1; //행의 최소값을 벗어나면 끝위치로
            if (currentX >= cards.GetLength(0)) currentX = 0; //행의 최대값을 벗어나면 0번 위치로
            else if (currentX < 0) currentX = cards.GetLength(0) - 1; //행의 최소값을 벗어나면 끝위치로

            UpgradeCursorPosition(currentX, currentY);
        }
    }
}


