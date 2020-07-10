
using UnityEngine;

class ExitButton : MonoBehaviour
{
    [SerializeField]
    protected GameObject window;
    public virtual void Click()
    {
        FindObjectOfType<SoundControl>().ExitButtonSound();
        window.SetActive(false);
    }
}
