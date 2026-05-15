using UnityEngine;
using UnityEngine.InputSystem;

namespace Shichuan
{
    public class Shichuan_CardSelector : MonoBehaviour
    {
        public Card[,] cards = new Card[5, 4]; //전체 카드
        public Card[] selectCards = new Card[2]; //선택된 카드

        public int count = 0; //고른카드 카운트
        public Transform cursor;
        public int currentIndexX = 0;
        public int currentIndexY = 0;

        private int selectCardIndex1X = 0;
        private int selectCardIndex1Y = 0;
        private int selectCardIndex2X = 0;
        private int selectCardIndex2Y = 0;

        public void Start()
        {
            CursorPosition();
        }

        private void Update()
        {
            if(Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                MoveXCursor(true);
            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                MoveXCursor(false);
            }
            else if( Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                MoveYCursor(true);
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                MoveYCursor(false);
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                SelectCard();
            }
        }

        public void CursorPosition()
        {
            float cardX = cards[currentIndexX, currentIndexY].transform.position.x;
            float cardY = cards[currentIndexX, currentIndexY].transform.position.y;
            cursor.position = new Vector3(cardX, cardY);
        }

        private void MoveXCursor(bool isLeft)
        {
            //int yValue = HasIndexCheck(true); //y좌표 전멸시 다음 y좌표로 이동용
            while (true)
            {
                currentIndexX += isLeft ? +1 : -1;
                if (currentIndexX < 0) currentIndexX = cards.GetLength(0) - 1;
                if (currentIndexX >= cards.GetLength(0)) currentIndexX = 0;

                if (cards[currentIndexX, currentIndexY] == null)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            CursorPosition();
        }
        private void MoveYCursor(bool isUp)
        {
            //int xValue = HasIndexCheck(false); //x좌표 전멸시 다음 x좌표 이동용
            while (true)
            {
                currentIndexY += isUp ? -1 : +1;
                if (currentIndexY < 0) currentIndexY = cards.GetLength(1) - 1;
                if (currentIndexY >= cards.GetLength(1)) currentIndexY = 0;

                if (cards[currentIndexX, currentIndexY] == null)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            CursorPosition();
        }
        public int HasIndexCheck(bool checkX) // 한줄 전체 삭제시 다른줄 이동용
        {
            int count = 0;

            if(checkX)
            {
                for (int i = 0; i < cards.GetLength(0); i++)
                {
                    if (cards[i,currentIndexY] != null)
                    {
                        count += 0;
                    }
                }
            }

            if(!checkX)
            {
                for (int i = 0; i < cards.GetLength(1); i++)
                {
                    if (cards[currentIndexX, i] != null)
                    {
                        count += 0;
                    }
                }
            }

            if (count > 0) return 0;
            else return 1;
        }

        private void SelectCard()
        {
            cards[currentIndexX, currentIndexY].Flip();

            if(count < 2)
            {
                SaveSelectCardInfo(currentIndexX, currentIndexY);
            }

            if(count == 2)
            {
                if (selectCards[0].myNumber == selectCards[1].myNumber)
                {
                    DeleteCard(selectCards[0], selectCards[1]);
                }
                else
                {
                    selectCards[0].Flip();
                    selectCards[1].Flip();
                }
                count = 0;
            }
        }

        private void SaveSelectCardInfo(int x, int y)
        { 
            if(count == 0)
            {
                selectCardIndex1X = x;
                selectCardIndex1Y = y;
            }
            else
            {
                selectCardIndex2X = x;
                selectCardIndex2Y = y;
            }
            selectCards[count] = cards[currentIndexX, currentIndexY];
            count++;
        }

        private void DeleteCard(Card a, Card b)
        {
            Destroy(a.gameObject); // a 컴포넌트가 부착된 게임오브젝트를 삭제합니다
            Destroy(b.gameObject); // b 컴포넌트가 부착된 게임오브젝트를 삭제합니다
            selectCards[0] = null;
            selectCards[1] = null;
            cards[selectCardIndex1X, selectCardIndex1Y] = null;
            cards[selectCardIndex2X, selectCardIndex2Y] = null;
        }
    }
}


