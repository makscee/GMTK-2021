using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Player player;
    public void Enable()
    {
        text.text = $"Blocks joined: {player.connectedBlocks.Count}";
        gameObject.SetActive(true);
    }
}