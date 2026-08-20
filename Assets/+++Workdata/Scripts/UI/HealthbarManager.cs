using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarManager : MonoBehaviour
{
   public static Action<int> OnHealthIncrease;
   public static Action<int> OnHealthDecrease;
   
   public Slider healthBar;
   public Slider delayedHealthBar;
   
   private bool _pullDelay = false;
   private float _wayToGo;
   public float sliderIncrements = 0.5f;
   private float _time;
   private float _timeToNext = 1f;

   private void Awake()
   {
      healthBar.value = healthBar.maxValue;
      delayedHealthBar.value = delayedHealthBar.maxValue;
   }

   private void OnEnable()
   {
      OnHealthDecrease += SetSliderDown;
      OnHealthIncrease += SetSliderUp;
   }

   private void OnDisable()
   {
      OnHealthDecrease -= SetSliderDown;
      OnHealthIncrease -= SetSliderUp;
   }

   private void Update()
   {
      if (_pullDelay)
      {
         _time += Time.deltaTime;
         if (_time >= _timeToNext)
         {
            if (delayedHealthBar.value == healthBar.value)
            {
               _pullDelay = false;
               return;
            }
            
            delayedHealthBar.value -= sliderIncrements;
         }
      }
   }

   private void SetSliderDown(int amount)
   {
      healthBar.value -= amount;

      StartCoroutine(DelayedHealthBar());

      if (healthBar.value <= 0)
      {
         healthBar.value = 0;
      }
   }

   private void SetSliderUp(int amount)
   {
      healthBar.value += amount;
      
      delayedHealthBar.value = healthBar.value;
      
      if (healthBar.value + amount > healthBar.maxValue)
      {
         healthBar.value = healthBar.maxValue;
      }
   }

   private IEnumerator DelayedHealthBar()
   {
      yield return new WaitForSeconds(0.5f);
      _pullDelay = true;
      _wayToGo = delayedHealthBar.value - healthBar.value;
   }
}
