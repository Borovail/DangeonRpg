using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EffectManager : Singleton<EffectManager>
    {
        public GameObject HpEffectIcon;
        public GameObject ArmorEffectIcon;

        private Vector3 _fountainPosition;
        public void ApplyFountainEffect(FountainType fountainType,float startTime,float repeatRate, Vector3 fountainPosition)
        {
            _fountainPosition = fountainPosition;

            switch (fountainType)
            {
                case FountainType.HealthRegeneration:
                    InvokeRepeating("HealthRegenerationEffect", startTime, repeatRate);
                    GameManager.Instance.PlayerGetsEffect(HpEffectIcon);
                    break;
                case FountainType.ArmorRegeneration:
                   InvokeRepeating("ArmorRegenerationEffect", startTime, repeatRate);
                    GameManager.Instance.PlayerGetsEffect(ArmorEffectIcon);
                    break;
            }
        }

        public void CancelFountainEffect(FountainType fountainType)
        {
            switch (fountainType)
            {
                case FountainType.HealthRegeneration:
                    CancelInvoke("HealthRegenerationEffect");
                    GameManager.Instance.PlayerEffectEnds(HpEffectIcon);
                    break;
                case FountainType.ArmorRegeneration:
                    CancelInvoke("ArmorRegenerationEffect");
                    GameManager.Instance.PlayerEffectEnds(ArmorEffectIcon);
                    break;
            }
        }


        public void ArmorRegenerationEffect()
        {
            GameManager.Instance.PlayerArmorChanged(1);
            FloatingTextManager.Instance.Show(new FloatingTextSettings("+1", 3, 4, Color.blue, Vector3.up /2, _fountainPosition, FloatingTextType.WorldSpaceFloatingText));
        }

        public void HealthRegenerationEffect()
        {
            GameManager.Instance.PlayerHealthChanged(1);
            FloatingTextManager.Instance.Show(new FloatingTextSettings("+1", 3, 4, Color.red, Vector3.up/2, _fountainPosition, FloatingTextType.WorldSpaceFloatingText));
        }
    }
}
