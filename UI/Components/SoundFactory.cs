using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: ComponentFactory(typeof(SoundFactory))]

namespace LiveSplit.UI.Components
{
    public class SoundFactory : IComponentFactory
    {
        public string ComponentName
        {
            get { return "Sound Effects"; }
        }

        public string Description
        {
            get { return "Plays sound effects for different situations."; }
        }

        public ComponentCategory Category
        {
            get { return ComponentCategory.Media; }
        }

        public IComponent Create(LiveSplitState state)
        {
            return new SoundComponent(state);
        }

        public string UpdateName
        {
            get { return ComponentName; }
        }

        public string XMLURL
        {
#if RELEASE_CANDIDATE
            get { return "http://livesplit.org/update_rc_sdhjdop/Components/update.LiveSplit.Sound.xml"; }
#else
            get { return "http://livesplit.org/update/Components/update.LiveSplit.Sound.xml"; }
#endif
        }

        public string UpdateURL
        {
#if RELEASE_CANDIDATE
            get { return "http://livesplit.org/update_rc_sdhjdop/"; }
#else
            get { return "http://livesplit.org/update/"; }
#endif
        }

        public Version Version
        {
            get { return Version.Parse("1.1.0"); }
        }
    }
}
