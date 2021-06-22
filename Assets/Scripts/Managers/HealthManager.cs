using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Sprite heartFullSprite;

    [SerializeField] private Image heartImage;

    [SerializeField] private CharacterController characterController;

    private List<Image> hearts = new List<Image>();


    private void OnEnable()
    {
        PlayerStatus.OnDamageTaken += UpdateHealthUI;
    }

    private void OnDisable()
    {
        PlayerStatus.OnDamageTaken -= UpdateHealthUI;
    }

    private void Start()
    {
        CreateHeartsUI();
    }

    private void CreateHeartsUI()
    {
        heartImage.sprite = heartFullSprite;
        hearts.Add(heartImage);
        for (int i = 1; i < characterController.maxHealth; i++)
        {
            Image nextHeart = Instantiate(heartImage, heartImage.transform.parent);
            nextHeart.transform.position = new Vector2(heartImage.transform.position.x + nextHeart.rectTransform.rect.width*i/2, heartImage.transform.position.y);
            nextHeart.transform.localScale = heartImage.transform.localScale;
            hearts.Add(nextHeart);
        }
    }

    private void UpdateHealthUI(int currentHealth)
    {
        for (int i = characterController.maxHealth; i > currentHealth; i--)
        {
            if (i - 1 < 0)
            {
                return;
            }
            hearts[i-1].gameObject.SetActive(false);
        }
           
    }
}
