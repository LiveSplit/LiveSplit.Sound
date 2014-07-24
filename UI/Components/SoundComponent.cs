using LiveSplit.Model;
using LiveSplit.Model.Comparisons;
using LiveSplit.TimeFormatters;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.UI.Components
{
    public class SoundComponent : LogicComponent
    {
        public LiveSplitState State { get; set; }

        public override string ComponentName
        {
            get { return "Sound Effects"; }
        }

        public SoundComponent(LiveSplitState state)
        {
            State = state;
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return null;
        }

        public override System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
        {
            return document.CreateElement("x");
        }

        public override void SetSettings(System.Xml.XmlNode settings)
        {
        }

        public override void RenameComparison(string oldName, string newName)
        {
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
        }
    }
}
