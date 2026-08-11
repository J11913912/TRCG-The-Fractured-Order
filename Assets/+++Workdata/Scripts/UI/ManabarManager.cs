using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManabarManager : MonoBehaviour
{
    public static Action<int> OnManaIncrease;
    public static Action<int> OnManaDecrease;
   
    public Slider manaBar;
    public Slider delayedManaBar;
   
    private bool _pullDelay = false;
    private float _wayToGo;
    public int sliderIncrements = 1;
    private float _time;
    private float _timeToNext = 0.5f;

    private void Awake()
    {
        manaBar.value = manaBar.maxValue;
        delayedManaBar.value = delayedManaBar.maxValue;
    }

    private void OnEnable()
    {
        OnManaDecrease += SetSliderDown;
        OnManaIncrease += SetSliderUp;
    }

    private void OnDisable()
    {
        OnManaDecrease -= SetSliderDown;
        OnManaIncrease -= SetSliderUp;
    }

    private void Update()
    {
        if (_pullDelay)
        {
            _time += Time.deltaTime;
            if (_time >= _timeToNext)
            {
                if (delayedManaBar.value == manaBar.value)
                {
                    _pullDelay = false;
                    return;
                }
            
                delayedManaBar.value -= sliderIncrements;
            }
        }
    }

    private void SetSliderDown(int amount)
    {
        manaBar.value -= amount;

        StartCoroutine(DelayedHealthBar());

        if (manaBar.value <= 0)
        {
            manaBar.value = 0;
        }
    }

    private void SetSliderUp(int amount)
    {
        manaBar.value += amount;
      
        delayedManaBar.value = manaBar.value;
      
        if (manaBar.value + amount > manaBar.maxValue)
        {
            manaBar.value = manaBar.maxValue;
        }
    }

    private IEnumerator DelayedHealthBar()
    {
        yield return new WaitForSeconds(1f);
        _pullDelay = true;
        _wayToGo = delayedManaBar.value - manaBar.value;
    }
}