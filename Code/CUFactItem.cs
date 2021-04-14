using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CUFactItem : MonoBehaviour
{
    public CUFacts fact;
    public FactUI factUI;
    public GameObject holder;
    public BoxCollider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
       
        holder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<BoxCollider2D>().IsTouching(playerCollider) && Input.GetButtonDown("Interact"))
        {
            holder.SetActive(true);
            factUI.title.SetText("CU Fact #" + fact.factNum);
            factUI.body.SetText(fact.factInfo);
            factUI.image.sprite = fact.factImage;
            Time.timeScale = 0;
            Destroy(gameObject);
        }
    }
}

[Serializable]
public class FactUI
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI body;
    public Image image;

    public FactUI(TextMeshProUGUI title, TextMeshProUGUI body, Image image)
    {
        this.title = title;
        this.body = body;
        this.image = image;
    }
}
