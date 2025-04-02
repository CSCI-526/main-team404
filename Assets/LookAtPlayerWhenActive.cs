using System.Collections;
using UnityEditor.Search;
using UnityEngine;

public class LookAtPlayerWhenActive : MonoBehaviour
{
    public bool isActive;
    private SpriteRenderer renderer;
    public float targetAlpha;
    public float transitionTime;
    private float currentAlpha;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
        currentAlpha = 0f;
        isActive = false;
    }

    
    private void Update()
    {
        // Approach target alpha based on whether it is active or not
        float targetAlphaValue = isActive ? targetAlpha : 0f;
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlphaValue, (targetAlpha / transitionTime) * Time.deltaTime);
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, currentAlpha);

        if (isActive)
        {
            Transform playerTransform = PlayerInfo.instance.player.transform;
            Vector2 direction = playerTransform.position - transform.position;
            transform.up = direction;
        }
    }

    public void Show()
    {
        isActive = true;
    }

    public void Hide()
    {
        isActive = false;
    }
}
