using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiTransformer : MonoBehaviour
{
    public Vector3Value Transition;
    public float TransitionTime;

    private RectTransform trans;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float alpha;

    private void Start()
    {
        trans = GetComponent<RectTransform>();

        startPosition = trans.anchoredPosition;
        endPosition = startPosition + Transition;
    }

    public void Forward()
    {
        StopAllCoroutines();
        StartCoroutine(forward());
    }

    private IEnumerator forward()
    {
        while (alpha<1)
        {
            trans.anchoredPosition = Vector3.Lerp(startPosition, endPosition, alpha);
            yield return null;
            alpha += Time.deltaTime / TransitionTime;
        }
        trans.anchoredPosition = endPosition;
    }

    public void Back()
    {
        StopAllCoroutines();
        StartCoroutine(back());
    }

    private IEnumerator back()
    {
        while (alpha > 0)
        {
            trans.anchoredPosition = Vector3.Lerp(startPosition, endPosition, alpha);
            yield return null;
            alpha -= Time.deltaTime / TransitionTime;
        }
        trans.anchoredPosition = startPosition;
    }
}
