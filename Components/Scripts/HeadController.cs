using UnityEngine;
using NewHorizons.Utility;

namespace Origins.Components.Scripts
{
    public class HeadController : MonoBehaviour
    {

        Animator animator;
        private bool ikActive = true;
        private Transform objTarget;

        void Start()
        {
            animator = GetComponent<Animator>();
            objTarget = SearchUtilities.Find("Player_Body").transform;
        }        

        private void OnAnimatorIK()
        {
            if (animator)
            {
                if (ikActive)
                {
                    if (objTarget != null)
                    {
                        animator.SetLookAtWeight(1);
                        animator.SetLookAtPosition(objTarget.position);
                    }
                    else
                    {
                        animator.SetLookAtWeight(0);
                    }
                }
            }

        }

    }
}
