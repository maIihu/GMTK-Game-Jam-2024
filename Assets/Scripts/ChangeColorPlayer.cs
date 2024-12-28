
using UnityEngine;

public class ChangeColorPlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite blueSp;
    [SerializeField] private Sprite redSp;
    [SerializeField] private Sprite yellowSp;

    [SerializeField] private Animator ani;
    [SerializeField] private RuntimeAnimatorController blueAni;
    [SerializeField] private RuntimeAnimatorController redAni;
    [SerializeField] private RuntimeAnimatorController yellowAni;

    public void BlueColorChange()
    {
        sr.sprite = blueSp;
        ani.runtimeAnimatorController = blueAni;
    }
    
    public void RedColorChange()
    {
        sr.sprite = redSp;
        ani.runtimeAnimatorController = redAni;
    }
    
    public void YellowColorChange()
    {
        sr.sprite = yellowSp;
        ani.runtimeAnimatorController = yellowAni;
    }
}
