using UnityEngine;
using UnityEngine.UI;

public class AnimControl : MonoBehaviour
{
    [SerializeField] private GameObject btn;

    public void TurnOnBtn()
    {
        btn.SetActive(true);
    }
}
