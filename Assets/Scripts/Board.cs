using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public bool isDone = false;

    public GameObject card;

    List<(GameObject, Vector3)> cardList = new List<(GameObject, Vector3)>();

    // Start is called before the first frame update
    void Start()
    {
        // isDone = false;  // pause timer

        if (GameManager.Instance.isHard == false) // easy 모드일 때
        {
            int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4 };
            arr = arr.OrderBy(x => Random.Range(0f, 4f)).ToArray();
            int backNumber = Random.Range(0, 4);
            for (int i = 0; i < 10; i++)
            {
                GameObject board = Instantiate(card, this.transform);

                float x = i % 3 * 1.4f - 1.4f;
                float y = i / 3 * 1.4f - 3.5f;
                if (i == 9)
                {
                    board.transform.localScale = new Vector2(1.2f, 1.2f);
                    // board.transform.position = new Vector2(0, y);
                    board.transform.position = new Vector2(0, 0);  // generate Card at the center of the scene
                    board.GetComponent<Card>().Setting(arr[i], backNumber);

                    // save card and its position data to cardList
                    cardList.Add((board, new Vector3(0f, y, 0f)));
                }
                else
                {
                    board.transform.localScale = new Vector2(1.2f, 1.2f);
                    // board.transform.position = new Vector2(x, y);
                    board.transform.position = new Vector2(0, 0);  // generate Card at the center of the scene
                    board.GetComponent<Card>().Setting(arr[i], backNumber);

                    // save card and its position data to cardList
                    cardList.Add((board, new Vector3(x, y, 0f)));
                }
            }

            // move generated Card to its place (sequentially)
            StartCoroutine(MoveCardSequential(cardList));

            // leftCards 업데이트
            GameManager.Instance.leftCards = arr.Length;
        }
        else
        {
            int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
            arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray();
            int backNumber = Random.Range(0, 4);
            for (int i = 0; i < 20; i++)
            {
                GameObject board = Instantiate(card, this.transform);

                float x = i % 4 * 1.4f-2.2f;
                float y = i / 4 * 1.4f-4f;

                board.transform.localScale = new Vector2(1.0f, 1.0f);
                // board.transform.position = new Vector2(x, y);
                board.transform.position = new Vector2(0, 0);  // generate Card at the center of the scene
                board.GetComponent<Card>().Setting(arr[i], backNumber);

                // save card and its position data to cardList
                cardList.Add((board, new Vector3(x, y, 0f)));

            }

            // move generated Card to its place (sequentially)
            StartCoroutine(MoveCardSequential(cardList));

            // leftCards 업데이트
            GameManager.Instance.leftCards = arr.Length;
        }

        // isDone = true;  // replay timer
    }

    // move generated cards to their position one at a time
    IEnumerator MoveCardSequential(List<(GameObject, Vector3)> cardList)
    {
        // for each card in cardList,
        // force other cards to wait until one is done
        foreach (var (card, pos) in cardList)
        {
            yield return StartCoroutine(MoveCard(card, pos));
        }
        yield return isDone = true;
    }

    // move generated cards from center to their position (at the same time)
    IEnumerator MoveCard(GameObject card, Vector3 pos)
    {
        // while card is not at pos (distance between card.position and targe position > 0.0f), 
        // move card to the position (pos) at speed of moveSpeed
        while (Vector3.Distance(card.transform.position, pos) > 0.25f)
        {
            // separate moveSpeed according to game difficulty
            float modifier = 7.5f;
            if (GameManager.Instance.isHard) { modifier = 7.5f * 1.25f; } // in Hard mode, card moves 25% faster

            // move directly to its position
            Vector3 direction = (pos - new Vector3 (0f,0f,0f)).normalized;
            card.transform.position += direction * moveSpeed * Time.deltaTime * modifier;
            yield return null;
        }
        // locate card at target position (pos)
        card.transform.position = pos;
    }
}
