using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    
    private Text text;
    public float HP = 100;
    //Think about these variables in future
    // public float shield = 100;
    // public float mana = 0;
    // public float HPRegenerationPerTik = 1;

    // public float additionalAttack = 1;
    // public float multipliedAttack = 1;
    // public float additionalAttackSpeed = 1;
    // public float multipliedAttackSpeed = 1;
    // public float criticalHitChance = 0;

    // public int itemsAmount = 0;
    // public GameItem[]/List<GameItem> items)

    // public float dashesAmount = 1;
    // public float dashLenght = 1;
    // public float additionalSpeed = 1;
    // public float multipliedSpeed = 1;
    // public float additionalSprintSpeed = 1;
    // public float multipliedSprintSpeed = 1;
    // public float additionaljumpPower = 1;
    // public float multipliedjumpPower = 1;

    // public float visionRadius = 1;
    // public float angleOfAvivableVision = 360; // 0-360
    // public float luck = 0;

    // public bool isClone = false;

    // public bool isFlying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<Text>();
        text.text = HP.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
