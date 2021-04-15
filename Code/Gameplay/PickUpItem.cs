using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpItem : MonoBehaviour
{
    public Weapon weapon;
    public Potion potion;

    private PlayerWeaponController playerWeapon;
    private BoxCollider2D selfCollider;

    [SerializeField]
    private TextMeshProUGUI itemText;
    

    // Start is called before the first frame update
    void Start()
    {
        playerWeapon = GameObject.Find("Player").GetComponent<PlayerWeaponController>();
        selfCollider = GetComponent<BoxCollider2D>();
        itemText = GameObject.Find("ItemPickupText").GetComponent<TextMeshProUGUI>();
        itemText.enabled = false;
    }

    // Update is called once per frame
    /// <summary>
    /// This method checks to see if the player is colliding with a item. If it does, then determine if
    /// the item is a weapon or a potion. If it's a potion, check to see what type of potion it is and
    /// effect the player's stats
    /// </summary>
    void Update()
    {
        if (selfCollider.IsTouching(playerWeapon.GetComponent<BoxCollider2D>())) {
            if (weapon != null)
            {
                itemText.text = "Press [E] or [U] For " + weapon.itemName;
                itemText.enabled = true;
                if (Input.GetButtonDown("Interact"))
                {
                    playerWeapon.equippedWeapon = weapon;
                    itemText.enabled = false;
                    Destroy(gameObject);
                }
            }
            else if (potion != null)
            {
                itemText.text = "Press [E] or [U] To Drink " + potion.itemName;
                itemText.enabled = true;
                if (Input.GetButtonDown("Interact")) {
                    switch (potion.potionType)
                    {
                        case Potion.PotionType.Health:
                            PlayerData.instance.AddHealth(25);
                            PlayerData.instance.GetComponent<SpriteRenderer>().color = Color.white;
                            break;
                        case Potion.PotionType.Speed:
                            CustomPlayerController.instance.drankSpeedPotion = true;
                            break;
                        case Potion.PotionType.Strength:
                            PlayerWeaponController.instance.drankStrengthPotion = true;
                            break;
                        case Potion.PotionType.Invisibility:
                            CustomPlayerController.instance.drankInvisPotion = true;
                            break;
                    }
                    itemText.enabled = false;
                    Destroy(gameObject);
                }
            }
            
        }
        else
        {
            itemText.enabled = false;
        }
    }
}
