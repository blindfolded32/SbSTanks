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
            _unit.TakeDamage +=HealthChanged;
        }

        private void HealthChanged(float currentHP)
        {
            Debug.Log($"{_unit.Parameters.Hp.GetCurrentHp}- {currentHP}");
          
            var damage = (_unit.Parameters.Hp.GetCurrentHp- currentHP)/_unit.Parameters.Hp.Max;
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
            Debug.Log($"Filled for {_foregroundImage.fillAmount}");
        }
    }  
}

