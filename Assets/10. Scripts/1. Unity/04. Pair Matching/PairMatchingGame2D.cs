using Study.CardSelector;
using UnityEngine;

namespace Study.ParMatchingGame
{
    public class PairMatchingGame2D : MonoBehaviour
    {
        [Header("Ref Object")]
        public CardSelector2D cardSelector2D;

        public Card[,] board2D;

        // 이 부분은 인스펙터에 노출해서 수정 가능하도록 설계합니다.
        public int columnCount = 5;
        public int rowCount = 4;
        public GameObject ClearObject;

        private int pairMatchingCount = 0; //페어매칭이 성공한 순간에 +2 되는 변수
        
        private void Awake()
        {
            // T는 타임 매개변수 입니다. 타임(클래스,컴포넌트등)을 넣으시면 됩니다.
            // .GetComponent<T>() : 나의 transform을 기준으로 T 객체(컴포넌트)를 검색해서 반환합니다.
            // .GetComponentInChildren<T>() : 나의 자식들을 기준으로 T 객체(컴포넌트)들을 검색해서 반환합니다.

            // .GetComponents<T>()
            // : 나의 transform을 기준으로 T 객체(컴포넌트)들을 검색해서 배열로 반환합니다.
            // .GetComponentsInChildren<T>()
            // : 나의 자식들을 기준으로 T 객체(컴포넌트)들을 검색해서 배열로 반환합니다.

            Card[] myChildrenCards = GetComponentsInChildren<Card>();
            // 나를 기준으로 내 자식 GameObject에 존재하는 Card 컴포넌트 들을
            // 찾아서 myChildrenCards에 대입해줘.

            Card[] cards = InitCardArray(myChildrenCards);

            // 아래부터는 2차원 배열 초기화 로직
            // cards를 2차원 배열로 쪼개 봅시다 Column의 갯수와 Row의 갯수가 필요합니다.

            board2D = new Card[columnCount, rowCount];
            for(int i = 0; i< cards.Length; i++)
            {
                int x = i % columnCount;
                int y = i / columnCount;
                board2D[x, y] = cards[i];
            }

            cardSelector2D.SetBoard(board2D);
            ClearObject.SetActive(false);

            // 매개변수로 전달된 Card 배열을 로직에 맞게 초기화 합니다.
            Card[] InitCardArray(Card[] cardArray)
            {
                int[] indexBuffer = new int[myChildrenCards.Length];

                for (int i = 0; i < indexBuffer.Length; i++)
                {
                    indexBuffer[i] = i;
                }

                // 인덱스 버퍼를 섞어줍니다.
                for (int i = indexBuffer.Length - 1; i > 0; i--)
                {
                    int j = Random.Range(0, i + 1);
                    int temp = indexBuffer[i];
                    indexBuffer[i] = indexBuffer[j];
                    indexBuffer[j] = temp;
                }

                for (int i = 0; i < myChildrenCards.Length; i += 2)
                {
                    int randNum = Random.Range((int)Card.Number.Two, (int)Card.Number.Ace + 1);
                    int indexA = indexBuffer[i];
                    int indexB = indexBuffer[i + 1];
                    myChildrenCards[indexA].myNumber = (Card.Number)randNum;
                    myChildrenCards[indexB].myNumber = (Card.Number)randNum;
                }

                return cardArray;
            }
        }

        private void LateUpdate()
        {
            if(cardSelector2D.wasSelectionCompleted)
            {
                Card[] selectedCard = cardSelector2D.GetSelectedCards();
                CheckPairMatching(selectedCard[0], selectedCard[1]);
            }
        }

        private void CheckPairMatching(Card a, Card b)
        {
            //같으면 지우고
            if(a.myNumber == b.myNumber)
            {
                DeleteCard(a);
                DeleteCard(b);

                pairMatchingCount += 2; //같으면 페어매칭카운트를 증가시킴
                CheckEnd();
            }
            else //다르면 뒤집어라
            {
                a.Flip();
                b.Flip();

                Debug.Log("두 카드가 다릅니다");
            }

            cardSelector2D.Clear();
        }

        // 객체를 안전하게 삭제하는 기능을 넣어봅시다.
        private void DeleteCard(Card target)
        {
            for(int y = 0; y < board2D.GetLength(1); y++)
            {
                for(int x = 0; x < board2D.GetLength(0); x++)
                {
                    if (board2D[x,y] == null) continue;
                    // Equals(매개변수) 함수는 "==" 같다고 생각해주세요 
                    if (board2D[x, y].Equals(target))
                    {
                        board2D[x, y] = null;
                        Destroy(target.gameObject);
                    }
                }
            }
        }

        private void CheckEnd()
        {
            if(pairMatchingCount >= board2D.Length)
            {
                //게임이 끝났다
                ClearObject.SetActive(true);
            }
        }
    }
}
