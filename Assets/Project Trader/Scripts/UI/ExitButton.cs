
using UnityEngine;

class ExitButton : MonoBehaviour
{
    [SerializeField]
    protected GameObject window;

    public virtual void Click()
    {
        window.SetActive(false);
    }
}
