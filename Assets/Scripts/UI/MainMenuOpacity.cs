using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOpacity : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] RectTransform[] itemsToRandomize;

    private CanvasGroup group;
    private Button button;
    private RectTransform rectTransform;

    //calculation variables
    public float t;
    public float t_whenClicked;
    public float opacity;
    public float opacity_whenClicked;
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
        if (group.alpha <= 0)
        {
            foreach (RectTransform item in itemsToRandomize)
            {
                RandomizeItemLocation(item);
            }
        }
        timeOfClick = Time.time;

        t_whenClicked = t;
        opacity_whenClicked = opacity;

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
        t = Time.time - timeOfClick;
        opacity = curve.Evaluate(t);

        // prevent weird clipping when clicking fast
        // sorry its weird and unreadable idk if im cooking readable code rn, all these lines are important tho
        if(t < t_whenClicked)
        {
            opacity = Mathf.Max(opacity_whenClicked, opacity);
        }

        group.alpha = opacity;


    }
}
