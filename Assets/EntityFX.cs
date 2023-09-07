using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMat;
    private Material originalMat;

    private void Start()
    {
        // Thay đổi màu sắc, và các thuộc tính khác của đối tượng
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    // Hiệu ứng nhân vật và kẻ địch khi tấn công nhau
    private IEnumerator FlashFX()
    {
        sr.material = hitMat;

        yield return new WaitForSeconds(flashDuration);

        sr.material = originalMat;
    }
}
