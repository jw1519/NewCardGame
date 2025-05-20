using Card;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class CardHand : MonoBehaviour
{
    public List<GameObject> cards = new();
    public SplineContainer splineContainer;
    public int amountOfCard;

    private void Awake()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            //cards[i].transform.localPosition = new Vector2(-200 + i * 70, 10);
            //cards[i].transform.localScale = new Vector2(1, 1);
        }
        UpdateCardPositions(0.15f);
    }
    public void AddCard(GameObject card)
    {
        cards.Add(card);
        UpdateCardPositions(0.15f);
    }
    public IEnumerator UpdateCardPositions(float duration)
    {
        if (cards.Count > 0)
        {
            float cardSpacing = 1 / amountOfCard;
            float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2;
            Spline spline = splineContainer.Spline;
            for (int i = 0; i < cards.Count; i++)
            {
                float x = firstCardPosition + i * cardSpacing;
                Vector3 splinePosition = spline.EvaluatePosition(x);
                Vector3 forward = spline.EvaluateTangent(x);
                Vector3 up = spline.EvaluateUpVector(x);
                Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);
                cards[i].transform.DOMove(splinePosition + transform.position + .01f * i * Vector3.back, duration);
                cards[i].transform.DORotate(rotation.eulerAngles, duration);
            }
            yield return new WaitForSeconds(duration);
        }
        else
        {
            yield break;
        }
            
    }
}
