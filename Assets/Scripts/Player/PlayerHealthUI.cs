using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))] 
public class PlayerHealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform targetTransform;

    Transform uiTransform;
    Image healthSlider;

    void Start()
    {
        uiTransform = Instantiate(uiPrefab, targetTransform).transform;
        uiTransform.SetParent(targetTransform);
        healthSlider = uiTransform.GetChild(0).GetComponent<Image>();

        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (uiTransform != null)
        {
            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;

            if (currentHealth <= 0)
            {
                Destroy(uiTransform.gameObject);
            }
        }
    }
}
