using UnityEngine;

public class TimeGivingComponent : MonoBehaviour
{
    [SerializeField] private float timeToGive;
    
    private void OnDisable()
    {
        EventManager.OnTimeAdded?.Invoke(timeToGive);
        EventManager.OnTimeGiverRequested?.Invoke(timeToGive, transform.position);
    }
}
