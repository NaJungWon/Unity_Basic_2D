using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI.Table;

namespace Shichuan
{
    public class Shichuan_CardSelector : MonoBehaviour
    {
        public enum Direction
        {
            Up, Down, Left, Right
        }

        public Card[,] cards; //전체 카드

        private Card selectCardA;
        private Card selectCardB;

        //이번 프레임에 카드 선택이 완료되었는지 체크하는 bool 변수
        public bool wasSelectionCompleted = false;

        public Transform cursor;

        public int currentIndexX = 0;
        public int currentIndexY = 0;

        public void Start()
        {
            CursorPosition();
        }

        private void Update()
        {
            wasSelectionCompleted = false;

            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
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

        private void MoveCursor(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    MoveYCursor(false);
                    break;
                case Direction.Down:
                    MoveYCursor(true);
                    break;
                case Direction.Left:
                    MoveXCursor(true);
                    break;
                case Direction.Right:
                    MoveXCursor(false);
                    break;
            }
        }

        private void MoveXCursor(bool isLeft)
        {
            int temp = currentIndexX;

            for(int i = 0; i < cards.GetLength(0); i++)
            {
                temp += isLeft ? +1 : -1;

                if (temp < 0) temp = cards.GetLength(0) - 1;
                if (temp >= cards.GetLength(0)) temp = 0;
                
                if (cards[temp, currentIndexY] == null)
                {
                    bool needYmove = HasIndexCheck(true); //y좌표 전멸시 다음 y좌표로 이동용
                    if(needYmove)
                    {
                        currentIndexY++;

                        if (currentIndexY < 0) currentIndexY = cards.GetLength(1) - 1;
                        if (currentIndexY >= cards.GetLength(1)) currentIndexY = 0;
                        if (cards[temp, currentIndexY] == null)
                        {
                            continue;
                        }
                    }
                    currentIndexX++;
                    continue;
                }
                else
                {
                    currentIndexX = temp;
                    CursorPosition();
                    break;
                }
            }
        }
        private void MoveYCursor(bool isUp)
        {
            int temp = currentIndexY;
            for (int i = 0; i < cards.GetLength(1); i++)
            {
                temp += isUp ? +1 : -1;

                if (temp < 0) temp = cards.GetLength(1) - 1;
                if (temp >= cards.GetLength(1)) temp = 0;
                

                if (cards[currentIndexX, temp] == null)
                {
                    bool needXmove = HasIndexCheck(false); //y좌표 전멸시 다음 y좌표로 이동용
                    if(needXmove)
                    {
                        currentIndexX++;

                        if (currentIndexX < 0) currentIndexX = cards.GetLength(0) - 1;
                        if (currentIndexX >= cards.GetLength(0)) currentIndexX = 0;
                        if (cards[currentIndexX, temp] == null)
                        {
                            continue;
                        }
                    }
                    currentIndexY++;
                    continue;
                }
                else
                {
                    currentIndexY = temp;
                    CursorPosition();
                    break;
                }
            }
        }
        private void CursorPosition()
        {
            float cardX = cards[currentIndexX, currentIndexY].transform.position.x;
            float cardY = cards[currentIndexX, currentIndexY].transform.position.y;
            cursor.position = new Vector3(cardX, cardY);
        }

        private bool HasIndexCheck(bool isXMove) // 행,열 둘 중 하나가 삭제되어 이동 불가할 때 다른줄 이동용
        {
            //int nullCount = 0;
            bool needCursorMove = false;

            if (isXMove) // 들어온 temp값이 x좌표 값일때
            {
                int nullCount = 0;
                for (int row = 0; row < cards.GetLength(0); row++)
                {
                    if (cards[row, currentIndexY] == null) nullCount++;
                }
                if (nullCount == cards.GetLength(0) - 1) needCursorMove = true;
            }
            else // 들어온 temp값이 y좌표 값일때
            {
                int nullCount = 0;
                for (int col = 0; col < cards.GetLength(1); col++)
                {
                    if (cards[currentIndexX, col] == null) nullCount++;
                }
                if (nullCount == cards.GetLength(1) - 1) needCursorMove = true;
            }
            
            return needCursorMove;
        }

        private void SelectCard()
        {
            Card currentCard = cards[currentIndexX, currentIndexY];

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

            cards[currentIndexX, currentIndexY].Flip();
        }
    }
}


