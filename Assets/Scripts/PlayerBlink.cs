using System.Collections;
using UnityEngine;

public class PlayerBlink : MonoBehaviour
{
    [SerializeField] private float blinkDuration = 2.5f;
    [SerializeField] private float blinkInterval = 0.08f;

    private SpriteRenderer[] spriteRenderers;
    private bool isBlinking;

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void StartBlink()
    {
        if (spriteRenderers == null || spriteRenderers.Length == 0)
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        if (spriteRenderers.Length > 0 && !isBlinking)
            StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        isBlinking = true;
        float elapsed = 0f;
        bool visible = true;

        while (elapsed < blinkDuration)
        {
            visible = !visible;
            float a = visible ? 1f : 0.2f;

            foreach (SpriteRenderer sr in spriteRenderers)
            {
                if (sr != null)
                {
                    Color c = sr.color;
                    c.a = a;
                    sr.color = c;
                }
            }

            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            if (sr != null)
            {
                Color c = sr.color;
                c.a = 1f;
                sr.color = c;
            }
        }

        isBlinking = false;
    }
}
