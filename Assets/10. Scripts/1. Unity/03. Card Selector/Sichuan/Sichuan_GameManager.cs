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
        public GameObject[,] cards = new GameObject[5, 4];
        public float cardSpacingX = 0.5f; //x좌표 카드 간격
        public float cardSpacingY = 0.7f; //y좌표 카드 간격

        int count = 0;
        int number = 0;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            //cardSelector = GetComponent<Shichuan_CardSelector>();
            SpawnCard();
            ShuffleCard();
            for (int i = 0; i < cards.GetLength(1); i++)
            {
                for(int j = 0; j < cards.GetLength(0);j++)
                {
                    cardSelector.cards[j, i] = cards[j, i].GetComponent<Card>();
                }
            }
            
        }

        void SpawnCard()
        {
            float centerX = (cards.GetLength(0) - 1) * 0.5f;
            float centerY = (cards.GetLength(1) - 1) * 0.5f;

            for (int i = 0; i < cards.GetLength(1); i++)
            {
                for(int j = 0; j < cards.GetLength(0); j++)
                {
                    float offsetX = (centerX - j) * cardSpacingX;
                    float offsetY = (centerY - i) * cardSpacingY;

                    Vector3 cardPosition = gameObject.transform.position + new Vector3(offsetX, offsetY, 0);
                    GameObject card = Instantiate(cardPrefab, cardPosition, Quaternion.identity);
                    Card cardInfo = card.gameObject.GetComponent<Card>();
                    ChangeCardInfo(cardInfo);
                    card.transform.parent = gameObject.transform;
                    cards[j, i] = card;
                    //Debug.Log($"{cards}배열 {j}, {i}좌표에 {card}오브젝트 삽입");
                }
            }
        }

        void ShuffleCard()
        {
            for (int i = cards.GetLength(1) - 1; 1 <= i; i--)
            {
                for (int j = cards.GetLength(0) - 1; 1 <= j; j--)
                {
                    int randomX = Random.Range(0, j);
                    int randomY = Random.Range(0, i);

                    GameObject temp = cards[j, i];
                    cards[j, i] = cards[randomX, randomY];
                    cards[randomX, randomY] = temp;
                }
            }
            float centerX = (cards.GetLength(0) - 1) * 0.5f;
            float centerY = (cards.GetLength(1) - 1) * 0.5f;

            for (int i = 0; i < cards.GetLength(1); i++)
            {
                for (int j = 0; j < cards.GetLength(0); j++)
                {
                    float offsetX = (centerX - j) * cardSpacingX;
                    float offsetY = (centerY - i) * cardSpacingY;

                    Vector3 cardPosition = transform.position + new Vector3(offsetX, offsetY, 0);

                    cards[j, i].transform.position = cardPosition;
                }
            }
        }

        void ChangeCardInfo(Card cardInfo)
        {
            if (count == 2)
            {
                count = 0;
                number++;
            }
            count++;
            cardInfo.InputNumber(number);
        }
    }
}
