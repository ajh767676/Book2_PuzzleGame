using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isPlaced = false;

    private TMP_Text pieceNumberText;
    private TMP_Text targetNumberText;

    void Start()
    {
        originalPosition = transform.position;

        string pieceNumber = gameObject.tag;
        GameObject ph = GameObject.Find("PH" + pieceNumber);

        pieceNumberText = CreateNumber(
            transform,
            pieceNumber,
            "PieceNumber",
            Color.white
        );

        if (ph != null)
        {
            targetNumberText = CreateNumber(
                ph.transform,
                pieceNumber,
                "TargetNumber",
                new Color(1f, 0.72f, 0.2f)
            );
        }
    }

    TMP_Text CreateNumber(
        Transform parent,
        string number,
        string objectName,
        Color color
    )
    {
        GameObject numberObject = new GameObject(
            objectName,
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(TextMeshProUGUI)
        );

        numberObject.transform.SetParent(parent, false);

        RectTransform rect =
            numberObject.GetComponent<RectTransform>();

        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        TextMeshProUGUI numberText =
            numberObject.GetComponent<TextMeshProUGUI>();

        numberText.text = number;
        numberText.fontSize = 28;
        numberText.fontStyle = FontStyles.Bold;
        numberText.alignment = TextAlignmentOptions.Center;
        numberText.color = color;
        numberText.outlineColor = Color.black;
        numberText.outlineWidth = 0.25f;
        numberText.raycastTarget = false;

        Canvas numberCanvas = numberObject.AddComponent<Canvas>();
        numberCanvas.overrideSorting = true;
        numberCanvas.sortingOrder = 50;

        return numberText;
    }

    public void InitCardPosition()
    {
        originalPosition = transform.position;
    }

    public void Drag(BaseEventData eventData)
    {
        if (isPlaced)
        {
            return;
        }

        PointerEventData pointerData =
            (PointerEventData)eventData;

        transform.position = pointerData.position;
    }

    public void Drop()
    {
        if (!isPlaced)
        {
            CheckMatch();
        }
    }

    public void CheckMatch()
    {
        GameObject img = gameObject;
        string tagNumber = gameObject.tag;
        GameObject ph = GameObject.Find("PH" + tagNumber);

        float distance = Vector3.Distance(
            ph.transform.position,
            img.transform.position
        );

        if (distance <= 50)
        {
            Snap(img, ph);
        }
        else
        {
            MoveBack();
        }
    }

    public void MoveBack()
    {
        transform.position = originalPosition;
    }

    public void Snap(GameObject img, GameObject ph)
    {
        img.transform.position = ph.transform.position;
        isPlaced = true;

        if (pieceNumberText != null)
        {
            pieceNumberText.gameObject.SetActive(false);
        }

        if (targetNumberText != null)
        {
            targetNumberText.gameObject.SetActive(false);
        }

        GameSessionManager gameManager =
            FindAnyObjectByType<GameSessionManager>();

        if (gameManager != null)
        {
            gameManager.PiecePlaced();
        }
    }
}