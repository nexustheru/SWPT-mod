using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace tesplugin 
{
    class floatingtext : MonoBehaviour
    {
        private GameObject textobject = new GameObject();
        private TextMesh texm;
        private Canvas texcan;
        private Image texim;
        public void setup()
        {
            texm = textobject.AddComponent<TextMesh>();
            texcan = textobject.AddComponent<Canvas>();
            texim = textobject.AddComponent<Image>();
            init();
        }
        private void init()
        {
            texm.color = Color.green;
            texm.fontSize = 13;
            texm.text = "testingggg";
            texim.fillAmount = 50f;
            print("trying floating text");
        
        }

        private void Update()
        {
               //texim.fillAmount = Mathf.Clamp(player.health / player.maxHealth, 0, 1f);
        }
      
        private void Start()
        {

            StartCoroutine(FadeTextToFullAlpha(1f, texm.color));

            textobject.transform.position = GameObject.Find("Player").transform.position;
            print("floating text courutine");
        }
        public IEnumerator FadeTextToFullAlpha(float t, Color i)
        {

            i = new Color(i.r, i.g, i.b, 0);
            while (i.a < 1.0f)
            {
                i = new Color(i.r, i.g, i.b, i.a + (Time.deltaTime / t));
                yield return null;
            }
        }

        public IEnumerator FadeTextToZeroAlpha(float t, Color i)
        {
            i = new Color(i.r, i.g, i.b, 1);
            while (i.a > 0.0f)
            {
                i = new Color(i.r, i.g, i.b, i.a - (Time.deltaTime / t));
                yield return null;
            }
        }
    }
}
