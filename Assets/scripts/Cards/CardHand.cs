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

    public IEnumerator AddCard(GameObject card)
    {
        cards.Add(card);
        yield return UpdateCardPositions(0.15f);
    }
    public IEnumerator UpdateCardPositions(float duration)
    {
        if (cards.Count == 0)
            yield break;

        float cardSpacing = 0.1f;
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;
        for (int i = 0; i < cards.Count; i++)
        {
            float x = firstCardPosition + i * cardSpacing;
            Debug.Log(x);
            
            Vector3 splinePosition = spline.EvaluatePosition(x);
            Vector3 forward = spline.EvaluateTangent(x);
            Vector3 up = spline.EvaluateUpVector(x);
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);
            cards[i].transform.DOMove(splinePosition + transform.position + .01f * i * Vector3.back, duration);
            cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }
        yield return new WaitForSeconds(duration);
    }
}
