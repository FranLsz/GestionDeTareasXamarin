using ClienteMovilGestionDeTareas.CustomControls;
using ClienteMovilGestionDeTareas.Droid.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TexBox), typeof(TexBoxDroid))]
namespace ClienteMovilGestionDeTareas.Droid.CustomControls
{
    public class TexBoxDroid : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            Control.SetTextColor(Android.Graphics.Color.BlueViolet);
            Control.SetBackgroundColor(Android.Graphics.Color.Coral);
        }
    }
}