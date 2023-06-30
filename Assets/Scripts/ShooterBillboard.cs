﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Scripts
{
    public class ShooterBillboard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private string prefix, postfix;
        [SerializeField] private float showTime = 2f;

        private Coroutine _showRoutine;

        private void Start()
        {
            text.enabled = false;
        }

        public void SetShooterText(int currentTarget)
        {
            if (_showRoutine != null) StopCoroutine(_showRoutine);
            _showRoutine = null;
            
            text.text = $"{prefix} {currentTarget} {postfix}";
            text.enabled = true;

            _showRoutine = StartCoroutine(Hide());

            IEnumerator Hide()
            {
                yield return new WaitForSeconds(showTime);
                text.enabled = false;
            }
        }
    }
}