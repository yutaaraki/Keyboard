using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{

    public RectTransform a_button;
    public RectTransform k_button;
    public RectTransform s_button;
    public RectTransform t_button;
    public RectTransform n_button;
    public RectTransform h_button;
    public RectTransform m_button;
    public RectTransform y_button;
    public RectTransform r_button;
    public RectTransform w_button;
    public RectTransform cancel_button;
    public RectTransform ok_button;

    public Text text_field;


    // Update is called once per frame
    void Update()
    {

    }
    public void MoveCursorNext()
    {
        current_button = "";
    }
    public void MoveCursorPrev()
    {
        digit--;
        text_field.text = text_field.text.Substring(0, digit);
        current_button = "";

    }
    char dakuten = '\x3099';   // U+3099: COMBINING KATAKANA-HIRAGANA VOICED SOUND MARK
    char handakuten = '\x309A';
    private Dictionary<string, List<string>> words = new Dictionary<string, List<string>>(){
        {"a", new List<string>() {"あ","い","う","え","お"}},
        {"k", new List<string>() {"か","き","く","け","こ"}},
        {"s", new List<string>() {"さ","し","す","せ","そ"}},
        {"t", new List<string>() {"た","ち","つ","て","と"}},
        {"n", new List<string>() {"な","に","ぬ","ね","の"}},
        {"h", new List<string>() {"は","ひ","ふ","へ","ほ"}},
        {"m", new List<string>() {"ま","み","む","め","も"}},
        {"y", new List<string>() {"や","ゆ","よ"}},
        {"r", new List<string>() {"ら","り","る","れ","ろ"}},
        {"w", new List<string>() {"わ","を","ん"}}
    };
    public int count = 0;
    public int digit = 0;
    public string current_button = "";

    private void Start()
    {
        text_field.text = "";
		Input.multiTouchEnabled = false;
    }
    public void ClickButton(string button)
    {
        switch (button)
        {
			case "clr":
				digit = 0;
				count = 0;
				current_button = "";
				text_field.text = "";
				break;
            case "small":
                digit++;
                text_field.text = text_field.text.Substring(0, digit - 1) + "っ";
                break;
            case "dakuten":
                var last = text_field.text[digit - 1];
                Debug.Log(last);

                var daku = words[current_button][count - 1] + dakuten;
                var handaku = words[current_button][count - 1] + handakuten;
                string daku_test = daku.Normalize(NormalizationForm.FormC);
                string handaku_test = handaku.Normalize(NormalizationForm.FormC);
                if (daku_test.Length == 1 && last.ToString() != daku_test && last.ToString() != handaku_test)
                {
                    text_field.text = text_field.text.Substring(0, digit - 1) + daku_test;

                }
                else if (handaku_test.Length == 1 && last.ToString() != handaku_test)
                {
                    text_field.text = text_field.text.Substring(0, digit - 1) + handaku_test;
                }
                else
                {
                    text_field.text = text_field.text.Substring(0, digit - 1) + words[current_button][count - 1];

                }
                break;
            case "ok":
                MoveCursorNext();
                break;
            case "cancel":
                MoveCursorPrev();
                break;
            default:
                if (current_button != button)
                {
                    count = 0;
                    digit++;
                }
                text_field.text = text_field.text.Substring(0, digit - 1) + words[button][count];
                count++;
                if (count >= words[button].Count)
                {
                    count = 0;
                }
                current_button = button;
                break;

        }

    }
}
