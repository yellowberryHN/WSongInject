using System;
using System.Windows.Forms;

namespace WSongInject
{
    public class DifficultyNUD : NumericUpDown
    {
        public override void UpButton()
        {
            if (this.Value == decimal.Zero)
                Value = decimal.One;
            else base.UpButton();
        }
        public override void DownButton()
        {
            if (this.Value == decimal.One)
                Value = decimal.Zero;
            else base.DownButton();
        }
    }
}
