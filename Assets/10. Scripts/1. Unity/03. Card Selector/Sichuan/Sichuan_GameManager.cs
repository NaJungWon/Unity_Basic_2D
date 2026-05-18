using Study.CardSelector;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Shichuan
{
    public class Sichuan_GameManager : MonoBehaviour
    {
        public Shichuan_CardSelector cardSelector;

        public GameObject cardPrefab; //카드 프리팹
        public Card[,] board = new Card[5, 4]; //2차원 게임보드
        public GameObject ClearObject;

        public float cardSpacingX = 0.5f; //x좌표 카드 간격
        public float cardSpacingY = 0.7f; //y좌표 카드 간격

        private int pairMatchingCount = 0; //페어매칭이 성공한 순간에 +2 되는 변수


        int count = 0;
        int number = 0;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            SpawnCard();
            ShuffleCard();
            cardSelector.SetBoard(board);
            ClearObject.SetActive(false);
        }

        void SpawnCard()
        {
            float centerX = (board.GetLength(0) - 1) * 0.5f;
            float centerY = (board.GetLength(1) - 1) * 0.5f;

            for (int col = 0; col < board.GetLength(1); col++)
            {
                for(int row = 0; row < board.GetLength(0); row++)
                {
                    float offsetX = (centerX - row) * cardSpacingX;
                    float offsetY = (centerY - col) * cardSpacingY;

                    Vector3 cardPosition = gameObject.transform.position + new Vector3(offsetX, offsetY, 0);
                    GameObject card = Instantiate(cardPrefab, cardPosition, Quaternion.identity);
                    Card cardInfo = card.GetComponent<Card>();
                    InputCardNumber(cardInfo);
                    cardInfo.Flip(); //테스트용
                    card.transform.parent = gameObject.transform;
                    board[row, col] = cardInfo;
                }
            }
        }

        void ShuffleCard()
        {
            for (int col = board.GetLength(1) - 1; 1 <= col; col--)
            {
                for (int row = board.GetLength(0) - 1; 1 <= row; row--)
                {
                    int randomX = Random.Range(0, row);
                    int randomY = Random.Range(0, col);

                    Card temp = board[row, col];
                    board[row, col] = board[randomX, randomY];
                    board[randomX, randomY] = temp;
                }
            }

            float centerX = (board.GetLength(0) - 1) * 0.5f;
            float centerY = (board.GetLength(1) - 1) * 0.5f;

            for (int col = 0; col < board.GetLength(1); col++)
            {
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    float offsetX = (centerX - row) * cardSpacingX;
                    float offsetY = (centerY - col) * cardSpacingY;

                    Vector3 cardPosition = transform.position + new Vector3(offsetX, offsetY, 0);

                    board[row, col].gameObject.transform.position = cardPosition;
                }
            }
        }

        void InputCardNumber(Card cardInfo)
        {
            if (count == 2)
            {
                count = 0;
                number++;
            }
            count++;
            cardInfo.InputNumber(number);
        }

        private void LateUpdate()
        {
            if (cardSelector.wasSelectionCompleted)
            {
                Card[] selectedCard = cardSelector.GetSelectedCards();
                CheckPairMatching(selectedCard[0], selectedCard[1]);
            }
        }

        private void CheckPairMatching(Card a, Card b)
        {
            //같으면 지우고
            if (a.myNumber == b.myNumber)
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
            for(int col = 0; col<board.GetLength(1); col++)
            {
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    if (board[row,col] == null) continue;
                    // Equals(매개변수) 함수는 "==" 같다고 생각해주세요 
                    if (board[row,col].Equals(target))
                    {
                        board[row,col] = null;      //먼저 배열에서 비워준 후
                        Destroy(target.gameObject); //Scene에서 삭제합니다.
                    }
                }
            }
        }

        // 게임이 끝났는지 여부를 검사하는 함수를 만들어봅시다
        private void CheckEnd()
        {
            if (pairMatchingCount >= board.Length)
            {
                ClearObject.SetActive(true);
            }
        }
    }
}
