using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] 
        public Image _foregroundImage;
        private float _imageUpdateSpeed = 1f;
        private UnitModel _unit;

        void Awake()
        {
            _unit = GetComponentInParent<UnitModel>();
//            _unit.TakeDamage +=HealthChanged;
        }

        private void HealthChanged(float currentHP)
        {
            Debug.Log($"{_unit.HP.GetCurrentHp}- {currentHP}");
          
            var damage = (_unit.HP.GetCurrentHp- currentHP)/_unit.HP.Max;
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

