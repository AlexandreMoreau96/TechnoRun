using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EasingManager : MonoBehaviour
{
    [SerializeField] private RectTransform m_scoreText;
    [SerializeField] private RectTransform m_gameOverText;

    public IEnumerator Test2()
    {
        float t = 0.0f;
        while (t <= 1)
        {
            t += Time.deltaTime / 8.0f;
            
            float test = EaseOutBounce(m_scoreText.localPosition.y, 0, t);
            m_scoreText.localPosition = new Vector3(m_scoreText.localPosition.x, test, m_scoreText.localPosition.z);

            yield return null;
        }
    }

    public IEnumerator Test()
    {
        float t = 0.0f;
        while (t <= 1)
        {
            t += Time.deltaTime / 8.0f;

            float vRotationY = EaseInOutBounce(m_scoreText.localPosition.y, 0, t) * Time.time;
            m_gameOverText.localRotation = Quaternion.Slerp(m_scoreText.rotation, new Quaternion(0, vRotationY, 0, 1), t);

            yield return null;
        }
    }

    /*
     * Ces fonctions sont copiées telle quelle du repo Github de " https://gist.github.com/cjddmut/d789b9eb78216998e95c" 
     */
    public float EaseOutBounce(float start, float end, float value)
    {
        value /= 1f;
        end -= start;
        if (value < (1 / 2.75f))
        {
            return end * (7.5625f * value * value) + start;
        }
        else if (value < (2 / 2.75f))
        {
            value -= (1.5f / 2.75f);
            return end * (7.5625f * (value) * value + .75f) + start;
        }
        else if (value < (2.5 / 2.75))
        {
            value -= (2.25f / 2.75f);
            return end * (7.5625f * (value) * value + .9375f) + start;
        }
        else
        {
            value -= (2.625f / 2.75f);
            return end * (7.5625f * (value) * value + .984375f) + start;
        }
    }

    public float EaseInOutBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        if (value < d * 0.5f) return EaseInBounce(0, end, value * 2) * 0.5f + start;
        else return EaseOutBounce(0, end, value * 2 - d) * 0.5f + end * 0.5f + start;
    }

    public float EaseInBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        return end - EaseOutBounce(0, end, d - value) + start;
    }
}
