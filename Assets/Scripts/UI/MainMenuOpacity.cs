using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MainMenuOpacity : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] RectTransform[] itemsToRandomize;

    private CanvasGroup group;
    private Button button;
    private RectTransform rectTransform;

    //calculation variables
    public float t;
    public float opacity;
    public float timeOfClick;
    float xSize;
    float ySize;


    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        rectTransform = GetComponent<RectTransform>();

        float xMargin = rectTransform.rect.width / 10;
        float yMargin = rectTransform.rect.height / 10;
        xSize = rectTransform.rect.width - xMargin;
        ySize = rectTransform.rect.height - yMargin;

        foreach (var item in itemsToRandomize)
        {
            RandomizeItemLocation(item);
        }

        timeOfClick = -10000;
    }

    void OnClick()
    {
        if (opacity <= 0)
        {
            foreach (var item in itemsToRandomize)
            {
                RandomizeItemLocation(item);
            }
        }
        timeOfClick = Time.time;
        

    }

    void RandomizeItemLocation(RectTransform item)
    {
        float x = Random.Range(-xSize/2, xSize/2 );
        float y = Random.Range(-ySize/2, ySize/2);
        item.anchoredPosition = new Vector2(x,y);
    }

    // who cares man
    private void Update()
    {
        float _t = t;
        t = Time.time - timeOfClick;
        opacity = curve.Evaluate(t);
        group.alpha = opacity;
    }
}
