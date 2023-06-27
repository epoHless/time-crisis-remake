using TMPro;
using UnityEngine;

public class LeaderboardEntryUI : MonoBehaviour
{
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text score;

    public void Init(string _name, string _score)
    {
        name.text = _name;
        score.text = _score;
    }
}
