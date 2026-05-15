using Study.CardSelector;
using UnityEngine;

namespace Study.ParMatchingGame
{
    public class PairMatchingGame : MonoBehaviour
    {
        // Card Selector에 의해 선택된 두 카드를 비교합니다.
        // 같으면 지우고, 다르면 뒤집습니다.
        [Header("Ref Object")]
        public CardSelector cardSelector;
        public Card[] board;// 게임 보드라는 의미입니다.
        public GameObject ClearObject;

        //CardSelector가 갖고있던 Card[]을 PairMachingGame 객체가 관리하도록
        //코드를 수정합니다. CardSelector는 외부에서 Card[]를 주입받아서
        //동작하게 됩니다.
        private int pairMatchingCount = 0; //페어매칭이 성공한 순간에 +2 되는 변수
        
        private void Awake()
        {
            int[] indexBuffer = new int[board.Length];

            for(int i = 0; i<indexBuffer.Length; i++)
            {
                //board의 인덱스를 넣어준다
                indexBuffer[i] = i;
            }

            // 인덱스 버퍼를 섞어줍니다.
            for(int i = indexBuffer.Length-1;i>0;i--)
            {
                int j = Random.Range(0, i + 1);
                int temp = indexBuffer[i];
                indexBuffer[i] = indexBuffer[j];
                indexBuffer[j] = temp;
            }

            for(int i = 0; i<board.Length; i+=2)
            {
                // Unity에서 랜덤을 사용하는 방법
                // .NET의 랜덤을 사용해도 되긴합니다만, 더 편한방법이 있습니다.

                int randNum = Random.Range((int)Card.Number.Two, (int)Card.Number.Ace + 1);
                //Random.Range(최소값, 최대값) 최소값~최대값 범위중에 임의의 수를 반환합니다
                //!! 주의 : 최대값은 해당 범위에 포함되지 않습니다.

                //인덱스 버퍼를 두개씩 가지고와서 randNu을 대입해줍니다.
                int indexA = indexBuffer[i];
                int indexB = indexBuffer[i + 1];
                board[indexA].myNumber = (Card.Number)randNum;
                board[indexB].myNumber = (Card.Number)randNum;

                //i, i+1을 적당히 셔플해서 뽑으면 됩니다.
            }


            cardSelector.SetBoard(board);
            ClearObject.SetActive(false);
        }

        private void LateUpdate()
        {
            if(cardSelector.wasSelectionCompleted)
            {
                Card[] selectedCard = cardSelector.GetSelectedCards();
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
                //Debug.Log("두 카드가 같습니다");
            }
            else //다르면 뒤집어라
            {
                a.Flip();
                b.Flip();

                Debug.Log("두 카드가 다릅니다");
            }

            cardSelector.Clear();
        }

        // 객체를 안전하게 삭제하는 기능을 넣어봅시다.
        private void DeleteCard(Card target)
        {
            
            // 선형 탐색을 이용해서 target의 위치를 찾습니다.
            for(int i = 0; i<board.Length; i++)
            {
                if (board[i] == null) continue;
                // Equals(매개변수) 함수는 "==" 같다고 생각해주세요 
                if (board[i].Equals(target))
                {
                    board[i] = null;            //먼저 배열에서 비워준 후
                    Destroy(target.gameObject); //Scene에서 삭제합니다.
                }
            }
        }

        // 게임이 끝났는지 여부를 검사하는 함수를 만들어봅시다
        private void CheckEnd()
        {
            if(pairMatchingCount >= board.Length)
            {
                //게임이 끝났다
                ClearObject.SetActive(true);

                // ~.SetActive(bool 매개변수)
                // 해당 게임오브젝트의 활성화/비활성화를 제어하는 함수입니다.
                // gameObject.SetActive(true); => 게임오브젝트가 활성화됩니다.
                // gameObject.SetActive(false); => 게임오브젝트가 비 활성화됩니다.
            }
        }
    }
}
