using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
    public class UnitHealthBar : MonoBehaviour
    {
        [SerializeField] 
        public Image _foregroundImage;
        private float _imageUpdateSpeed = 1f;
        private AbstractUnit _unit;

        private void Awake()
        {
            _unit = GetComponentInParent<AbstractUnit>();
            _unit.TakeDamage +=HealthChanged;
        }

        private void HealthChanged(float currentHP)
        {
          //  Debug.Log($"{_unit.Controller.Model.HP.GetCurrentHp}- {currentHP}");
          var damage = (_unit.Controller.Model.HP.GetCurrentHp - currentHP)/_unit.Controller.Model.HP.Max;
            StartCoroutine(ChangeHealthPicture(damage));
        }

        public void ResetBar(float maxVal)
        {
            StartCoroutine(ChangeHealthPicture(maxVal));
        }

        public void RenewBar(AbstractUnit unit)
        {
            var filled = _unit.Controller.Model.HP.GetCurrentHp /_unit.Controller.Model.HP.Max;
            StartCoroutine(ChangeHealthPicture(filled));
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
           // Debug.Log($"Filled for {_foregroundImage.fillAmount}");
        }
    }  
}

