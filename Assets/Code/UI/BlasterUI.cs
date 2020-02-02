using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlasterUI : MonoBehaviour
{
    [SerializeField] private Image blasterImage;

    [Header("Blaster Color")]
    [SerializeField] private Color initialColor;
    [SerializeField] private Color finalColor;

    [Header("Scale")]
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private bool invertScaleInX;

    public void SetChargeState(float charge)
    {//charge is normalized
        blasterImage.color = Color.Lerp(initialColor, finalColor, charge);
        float scale = Mathf.Lerp(minScale, maxScale, charge);
        transform.localScale = new Vector3(scale * (invertScaleInX?-1:1), scale, scale);
    }
}
