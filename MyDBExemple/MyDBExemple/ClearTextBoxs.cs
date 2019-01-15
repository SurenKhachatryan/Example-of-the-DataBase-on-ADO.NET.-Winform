using System.Windows.Forms;

namespace MyDBExemple
{
    static class ClearTextBoxs
    {
        public static void Clear(TextBox[] textBoxArr)
        {
            for (int i = 0; i < textBoxArr.Length; i++)
            {
                textBoxArr[i].Text = "";
            }
        }
    }
}
