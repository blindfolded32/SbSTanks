using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace SbSTanks
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] 
        private Image _foregroundImage;
        private float _imageUpdateSpeed = 1f;
        private Unit _unit;

        void Awake()
        {
            _unit = GetComponentInParent<Unit>();
           // _enemy.HealthChanged += HealthChanged;
           _unit.TakeDamage += (x)=>
           {
               HealthChanged(_unit.Parameters.Hp.GetCurrentHp - x);
           };
        }

        private void HealthChanged(float currentHP)
        {
            Debug.Log($"ТЕКУЩЕЕ Здоровье !!! ---- {_unit.Parameters.Hp.GetCurrentHp}");
            Debug.Log($"ТЕКУЩЕЕ Здоровье из Action'a!!! ---- {currentHP}");
            Debug.Log($"МАКС Здоровье !!! ---- {_unit.Parameters.Hp.Max}");
            var damage = 0.4f;
            StartCoroutine(ChangeHealthPicture(damage));
        }

        private IEnumerator ChangeHealthPicture(float currentHP)
        {
            var fullPictureHP = _foregroundImage.fillAmount;
            var elapsed = 0f;
            
            while (elapsed < _imageUpdateSpeed)
            {
                elapsed += Time.deltaTime;
                _foregroundImage.fillAmount = Mathf.Lerp(fullPictureHP, currentHP, elapsed / _imageUpdateSpeed);
                yield return null;
            }
            _foregroundImage.fillAmount = currentHP;
        }
    }  
}

