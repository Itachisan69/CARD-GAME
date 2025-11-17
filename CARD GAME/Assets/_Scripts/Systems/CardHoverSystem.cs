using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHoverSystem : Singelton<CardHoverSystem> 
{
    [SerializeField] private CardView cardHoverView;

    public void Show(Card card, Vector3 position)
    {
        cardHoverView.gameObject.SetActive(true);
        cardHoverView.Setup(card);
        cardHoverView.transform.position = position;
    }

    public void Hide()
    {
        cardHoverView.gameObject.SetActive(false);
    }
}
