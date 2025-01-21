using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject card;
    // Start is called before the first frame update
    void Start()
    {
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
                    board.transform.position = new Vector2(0, y);
                    board.GetComponent<Card>().Setting(arr[i], backNumber);
                }
                else
                {
                    board.transform.localScale = new Vector2(1.2f, 1.2f);
                    board.transform.position = new Vector2(x, y);
                    board.GetComponent<Card>().Setting(arr[i], backNumber);
                }

            }
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
                board.transform.position = new Vector2(x, y);
                board.GetComponent<Card>().Setting(arr[i], backNumber);

            }
        }
    }


}
