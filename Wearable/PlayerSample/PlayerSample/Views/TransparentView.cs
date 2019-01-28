using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using System.Runtime.InteropServices;

[assembly: ExportRenderer(typeof(PlayerSample.TransparentView), typeof(PlayerSample.TransparentViewRenderer))]

namespace PlayerSample
{
    public class TransparentView : View
    {

    }

    public class TransparentViewRenderer : ViewRenderer<TransparentView, ElmSharp.Box>
    {
        ElmSharp.Rectangle _transparent;
        protected override void OnElementChanged(ElementChangedEventArgs<TransparentView> e)
        {
            if (Control == null)
            {
                SetNativeControl(new ElmSharp.Box(Forms.NativeParent));
                _transparent = new ElmSharp.Rectangle(Forms.NativeParent);
                _transparent.Color = ElmSharp.Color.Transparent;
                _transparent.Show();
                Control.SetLayoutCallback(OnLayout);
                Control.PackEnd(_transparent);
                MakeTransparent();
            }

            base.OnElementChanged(e);
        }

        void OnLayout()
        {
            _transparent.Geometry = Control.Geometry;
        }
        void MakeTransparent()
        {
            try
            {
                evas_object_render_op_set(_transparent.RealHandle, 2);
            }
            catch (Exception e)
            {
            }
        }
        [DllImport("libevas.so.1")]
        internal static extern void evas_object_render_op_set(IntPtr obj, int op);
    }
}
