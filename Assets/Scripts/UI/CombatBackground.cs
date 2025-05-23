using UnityEngine;

public class BackgroundAnimator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        // Get the current biome from GameController and convert it to int
        int biomeIndex = (int)GameController.Instance.CurrentBiomeState;
        animator.SetInteger("BiomeIndex", biomeIndex);
    }

    // void Update()
    // {
    //     
    // }
}