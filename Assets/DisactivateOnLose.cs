using UnityEngine;
using Zenject;

public class DisactivateOnLose : MonoBehaviour
{
    [Inject] PeopleLikeService _peoplelikeServie;

    private void Start()
    {
        _peoplelikeServie.OnZeroLikes += Disactivate;
    }

    private void Disactivate()
    {
        gameObject.SetActive(false);
    }
}
