using UnityEngine;

public class BonusTank : MonoBehaviour {

    SpriteRenderer body;
    bool bonusTank;
    public bool IsBonusTankCheck()
    {
        return bonusTank;
    }
    public void MakeBonusTank()
    {
        body = gameObject.GetComponent<SpriteRenderer>();
        bonusTank = true;
        InvokeRepeating("Blink", 0, 0.3f);
    }

    private void Blink()
    {
        if (body.color == Color.white)
        {
            body.color = Color.Lerp(Color.white, Color.red, 0.4f);
        }
        else
        {
            body.color = Color.white;
        }
    }
}
