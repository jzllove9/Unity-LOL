using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WarningWindow : MonoBehaviour
{
    [SerializeField]
    private Text text;

    WarningResult result;

    public void active(WarningModel model)
    {
        text.text = model.value;
        this.result = model.result;
        this.gameObject.SetActive(true);
    }

    public void close()
    {
        this.gameObject.SetActive(false);
        if (result != null)
        {
            result();
        }
    }
}
