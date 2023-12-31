﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using NewHorizons.Utility;
using System.Collections;

namespace Origins.Components.Scripts
{
    public class SmoothMinimiser : MonoBehaviour
    {
        public static SmoothMinimiser Instance;

        private void Start()
        {
            Instance = this;          
        }

        public void StartEvent()
        {
            StartCoroutine(StartMinimise());           
        }

        private IEnumerator StartMinimise()
        {    
            for (int i = 0; i < 20000 ; i++)
            {
                gameObject.transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
                yield return new WaitForSeconds(0.001f);
            }
        }

    }
}
