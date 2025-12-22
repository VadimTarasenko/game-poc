using UnityEngine;

public interface IInteractable
{
    void OnHeroEnterRange(GameObject hero);
    void OnHeroExitRange(GameObject hero);
    Transform GetTransform();
}
