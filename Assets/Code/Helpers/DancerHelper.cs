using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public sealed class DancerHelper : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float durationMin = 0.1f;
    [SerializeField] private float durationMax = 0.2f;
    [SerializeField] private float rotationMin = 5;
    [SerializeField] private float rotationMax = 14;

    private Tween m_danceTween;
    private Coroutine m_danceCoroutine;
    
    //backup 
    private Quaternion initialRotation;
    private bool isDancing = false;

    public void StartDanceAnimation()
    {
        StartDanceAnimation(durationMin, durationMax, rotationMin, rotationMax);
    }

    public void StartDanceAnimation(DanceHelperConfig config)
    {
        StartDanceAnimation(config.durationMin, config.durationMax, config.rotationMin, config.rotationMax);
    }

    private void StartDanceAnimation(float durationMin, float durationMax, float rotationMin, float rotationMax)
    {
        if (!isDancing)
        {
            //copy state
            this.durationMin = durationMin;
            this.durationMax = durationMax;
            this.rotationMin = rotationMin;
            this.rotationMax = rotationMax;
            //Start!
            isDancing = true;
            initialRotation = transform.rotation;
            m_danceCoroutine = StartCoroutine(DanceAnimation());
        }
    }

    public void StopDanceAnimation()
    {
        if(isDancing)
        {
            StopCoroutine(m_danceCoroutine);
            m_danceTween.Kill();
            m_danceTween = null;
            transform.rotation = initialRotation;
            isDancing = false;
        }
    }

    private IEnumerator DanceAnimation()
    {
        int factor = 1;
        while (true)
        {
            float duration = Random.Range(durationMin, durationMax);
            m_danceTween = transform.DORotate(new Vector3(0, 0, Random.Range(rotationMin * factor, rotationMax * factor)), duration);
            yield return new WaitForSeconds(duration);
            factor *= -1;
        }
    }

    [System.Serializable]
    public class DanceHelperConfig
    {
        public float durationMin;
        public float durationMax;
        public float rotationMin;
        public float rotationMax;
    }
}
