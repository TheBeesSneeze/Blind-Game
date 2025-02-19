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

    /// <summary>
    /// Called when main menu -> credits / htp
    /// </summary>
    public void HideAll()
    {
        group.alpha = 0;
        timeOfClick = -10000;
    }

    public void OnClick()
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
        float x = Random.Range(-WIDTH/5, WIDTH/5);
        float y = Random.Range(-HEIGHT/5, HEIGHT/5);
        item.anchoredPosition = new Vector2(x,y);
    }

    /// <summary>
    /// Awesome O(n^3+) algorithm that can potentially run forever i think
    /// </summary>
    private void ValidateComponentDistances()
    {
        bool complete = true;
        float distance;
        while (complete)
        {
            complete = true;



            for (int i = 0; i < itemsToRandomize.Length; i++)
            {
                // Title is seperate from buttons

                for (int j = 0; j < itemsToRandomize.Length; j++)
                {
                    if (i == j)
                        continue;

                    distance = Vector3.Distance(itemsToRandomize[i].position, itemsToRandomize[j].position);

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
