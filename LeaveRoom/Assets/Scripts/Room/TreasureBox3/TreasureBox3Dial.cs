using TMPro;
using UnityEngine;

public class TreasureBox3Dial : MonoBehaviour
{
    const int MaxNumber = 9;
    
    int currentNumber = 0;
    public int CurrentNumber => currentNumber;

    [SerializeField] TMP_Text text;
    
    public void Up()
    {
        currentNumber = (currentNumber + 1) % (MaxNumber + 1);
        UpdateText();
    }

    public void Down()
    {
        currentNumber = (currentNumber + MaxNumber) % (MaxNumber + 1);
        UpdateText();
    }

    void UpdateText()
    {
        text.text = currentNumber.ToString();
    }
}