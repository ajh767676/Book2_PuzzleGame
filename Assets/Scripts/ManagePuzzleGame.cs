using UnityEngine;
using UnityEngine.UI;

public class ManagePuzzleGame : MonoBehaviour
{
    public Image piece;
    public Image placeHolder;

    float phWidth;
    float phHeight;

    float timer;
    bool cardsShuffled = false;

    void Start()
    {
        CreatePlaceHolders();
        CreatePieces();  
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 4 && !cardsShuffled)
        {
            ShufflePieces();
            cardsShuffled = true;
        }
    }

    void ShufflePieces()
    {
        int[] newArray = new int[25];

        for (int i = 0; i < 25; i++)
        {
            newArray[i] = i;
        }

        int tmp;

        for (int t = 0; t < 25; t++)
        {
            tmp = newArray[t];
            int r = Random.Range(t, 25);
            newArray[t] = newArray[r];
            newArray[r] = tmp;
        }

        for (int i = 0; i < 25; i++)
        {
            float row;
            float column;
            float nbRows = 5;
            float nbColumns = 5;

            row = newArray[i] % 5;
            column = newArray[i] / 5;

            Vector3 centerPosition =
                GameObject.Find("leftSide").transform.position;

            GameObject g = GameObject.Find("Piece" + (i + 1));

            Vector3 newPosition = new Vector3(
                centerPosition.x + phWidth * (row - nbRows / 2),
                centerPosition.y - phHeight * (column - nbColumns / 2),
                centerPosition.z
            );

            g.transform.position = newPosition;
            g.GetComponent<DragAndDrop>().InitCardPosition();
        }
    }

    public void CreatePlaceHolders()
    {
        phWidth = 100;
        phHeight = 100;

        float nbRows;
        float nbColumns;

        nbRows = 5;
        nbColumns = 5;

        for (int i = 0; i < 25; i++)
        {
            Vector3 centerPosition = GameObject.Find("rightSide").transform.position;

            float row;
            float column;

            row = i % 5;
            column = i / 5;

            Vector3 phPosition = new Vector3(
                centerPosition.x + phWidth * (row - nbRows / 2),
                centerPosition.y - phHeight * (column - nbColumns / 2),
                centerPosition.z
            );

            Image ph = Instantiate(
                placeHolder,
                phPosition,
                Quaternion.identity
            );

            ph.tag = "" + (i + 1);
            ph.name = "PH" + (i + 1);
            ph.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }
    public void CreatePieces()
    {
        phWidth = 100;
        phHeight = 100;

        float nbRows;
        float nbColumns;

        nbRows = 5;
        nbColumns = 5;

        for (int i = 0; i < 25; i++)
        {
            Vector3 centerPosition = GameObject.Find("leftSide").transform.position;

            float row;
            float column;

            row = i % 5;
            column = i / 5;

            Vector3 piecePosition = new Vector3(
                centerPosition.x + phWidth * (row - nbRows / 2),
                centerPosition.y - phHeight * (column - nbColumns / 2),
                centerPosition.z
            );

            Image newPiece = Instantiate(
                piece,
                piecePosition,
                Quaternion.identity
            );

            newPiece.tag = "" + (i + 1);
            newPiece.name = "Piece" + (i + 1);
            newPiece.transform.SetParent(GameObject.Find("Canvas").transform);

            Sprite[] allSprites = Resources.LoadAll<Sprite>("lion");
            Sprite selectedSprite = allSprites[i];
            newPiece.sprite = selectedSprite;
        }
    }
}