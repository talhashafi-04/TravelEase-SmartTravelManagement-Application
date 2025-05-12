//using System.Drawing;
//using System.Windows.Forms;

//public class TransparentPanel : Panel
//{
//    public TransparentPanel()
//    {
//        SetStyle(ControlStyles.SupportsTransparentBackColor |
//                ControlStyles.OptimizedDoubleBuffer |
//                ControlStyles.AllPaintingInWmPaint |
//                ControlStyles.UserPaint, true);
//        BackColor = Color.Transparent;
//    }

//    protected override CreateParams CreateParams
//    {
//        get
//        {
//            CreateParams cp = base.CreateParams;
//            cp.ExStyle |= 0x20; // WS_EX_TRANSPARENT
//            return cp;
//        }
//    }

//    protected override void OnPaint(PaintEventArgs e)
//    {
//        if (e.Graphics != null)
//        {
//            using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, 0, 0, 0)))
//            {
//                e.Graphics.FillRectangle(brush, this.ClientRectangle);
//            }
//        }
//        base.OnPaint(e);
//    }

//    protected override void OnPaintBackground(PaintEventArgs e)
//    {
//        // Don't paint background
//    }
//}


using System.Drawing;
using System.Windows.Forms;

public class TransparentPanel : Panel
{
    private int opacity = 50;

    public TransparentPanel()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        SetStyle(ControlStyles.Opaque, false);
        SetStyle(ControlStyles.ResizeRedraw, true);
        BackColor = Color.Transparent;
    }

    public int Opacity
    {
        get { return opacity; }
        set
        {
            if (value >= 0 && value <= 100)
            {
                opacity = value;
                Invalidate();
            }
        }
    }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x20; // WS_EX_TRANSPARENT
            return cp;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        using (SolidBrush brush = new SolidBrush(Color.FromArgb((opacity * 255) / 100, Color.Black)))
        {
            e.Graphics.FillRectangle(brush, ClientRectangle);
        }
        base.OnPaint(e);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // Don't paint background
    }
}