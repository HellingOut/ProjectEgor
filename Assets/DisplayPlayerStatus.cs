using UnityEngine;
using TMPro; // Для работы с TextMeshPro

public class PlayerUI : MonoBehaviour
{
    public PlayerLogic player;
    public TMP_Text healthText;
    void Start(){
        
    }
    void Update()
    {
        healthText.text = "HP: " + player.HP.ToString();
    }
}