using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public const short MAX_HEALTH = 100;

    public short playerHealth;
    public short PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public PlayerLogin PlayerLogin { get; set; }
    public short PlayerAttack { get; set; }
    public short NumEnemiesKilled { get; set; }

    private float TimeSinceLastHit { get; set; }
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private TextMeshProUGUI currentHealthText;
    [SerializeField]
    private TextMeshProUGUI maxHealthText;

    public PlayerData(short playerHealth, short playerAttack)
    {
        PlayerHealth = playerHealth;
        PlayerAttack = playerAttack;
    }

    private void Start()
    {
        instance = this;
        PlayerHealth = MAX_HEALTH;
        TimeSinceLastHit = Time.time;
        currentHealthText.text = playerHealth.ToString();
        maxHealthText.text = playerHealth.ToString();
        healthSlider.maxValue = MAX_HEALTH;
        healthSlider.minValue = 0;
        healthSlider.value = playerHealth;
    }

    public void ResetHealth()
    {
        PlayerHealth = MAX_HEALTH;
    }

    public void TakeDamage(short damage)
    {
        if (Mathf.Abs(Time.time - TimeSinceLastHit) >= 1f)
        {
            TimeSinceLastHit = Time.time;
            StartCoroutine(ShowDamageColor());
            PlayerHealth -= damage;
            Debug.Log("Player Health: " + PlayerHealth);
            transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
            if (PlayerHealth <= 0)
            {
                CustomPlayerController.instance.canMove = false;
                healthSlider.value = playerHealth;
                currentHealthText.text = playerHealth.ToString();
                Debug.Log("Player is downed");
                GetComponent<Animator>().SetBool("isDead", true);
                GetComponent<Animator>().SetBool("isMoving", false);
                GetComponent<LowHealthPulse>().enabled = false;
                GetComponent<SpriteRenderer>().color = Color.red;
                Animator fadeAnim = GameObject.Find("BlackFade").GetComponent<Animator>();
                fadeAnim.SetTrigger("Fade");
                return;
            }
            else if (PlayerHealth <= 20)
            {
                currentHealthText.color = Color.red;
                GetComponent<LowHealthPulse>().enabled = true;
            }
            healthSlider.value = playerHealth;
            currentHealthText.text = playerHealth.ToString();
        }
    }

    public IEnumerator ShowDamageColor()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.red, 1f);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.white, 1f);
    }

    public short AddHealth(short num)
    {
        playerHealth += num;
        if (playerHealth >= MAX_HEALTH)
            playerHealth = 100;

        healthSlider.value = playerHealth;
        currentHealthText.text = playerHealth.ToString();
        return PlayerHealth;
    }
}

[Serializable]
public class PlayerLogin
{
    public string username;
    public string password;
}
