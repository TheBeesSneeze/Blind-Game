using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOpacity : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] RectTransform[] itemsToRandomize;
    [SerializeField] float minDistanceBetweenComponents;
    [SerializeField] private CanvasGroup group;

    private Button button;
    private RectTransform rectTransform;

    //calculation variables

    private const float WIDTH = 1920;
    private const float HEIGHT = 1080;

    private float t;
    private float t_whenClicked;
    private float opacity;
    private float opacity_whenClicked;
    private float timeOfClick;


    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        rectTransform = GetComponent<RectTransform>();

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
            ValidateComponentDistances();
        }
        timeOfClick = Time.time;

        t_whenClicked = t;
        opacity_whenClicked = opacity;

    }

    void RandomizeItemLocation(RectTransform item)
    {
        float x = Random.Range(-WIDTH/4, WIDTH/4);
        float y = Random.Range(-HEIGHT/4, HEIGHT/4);
        item.anchoredPosition = new Vector2(x,y);
    }

    private void ValidateComponentDistances()
    {
        bool complete = true;
        while (complete)
        {
            complete = true;
            for (int i = 0; i < itemsToRandomize.Length; i++)
            {
                for (int j = 0; j < itemsToRandomize.Length; j++)
                {
                    if (i == j)
                        continue;

                    float distance = Vector3.Distance(itemsToRandomize[i].position, itemsToRandomize[j].position);

                    // if two components are too close
                    if (distance < minDistanceBetweenComponents)
                    {
                        complete = false;
                        Debug.Log(distance);
                        DriftComponentsAway(itemsToRandomize[i], itemsToRandomize[j]);
                    }
                        
                }
            }

            if (complete)
                return;
        }
        
    }

    private void DriftComponentsAway(RectTransform obj1, RectTransform obj2)
    {
        Vector3 direction = obj1.position - obj2.position;

        obj1.position += direction;
        obj2.position -= direction;
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

        group.interactable = (group.alpha > 0);



    }
}
