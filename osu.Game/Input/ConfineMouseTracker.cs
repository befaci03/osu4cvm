// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Game.Configuration;
using osu.Game.Screens.Play;

namespace osu.Game.Input
{
    /// <summary>
    /// Connects <see cref="OsuSetting.ConfineMouseMode"/> with <see cref="FrameworkSetting.ConfineMouseMode"/>.
    /// If <see cref="ILocalUserPlayInfo.PlayingState"/> is playing, we should also confine the mouse cursor if it has been
    /// requested with <see cref="OsuConfineMouseMode.DuringGameplay"/>.
    /// </summary>
    public partial class ConfineMouseTracker : Component
    {
        private Bindable<ConfineMouseMode> frameworkConfineMode;
        private Bindable<WindowMode> frameworkWindowMode;
        private Bindable<bool> frameworkMinimiseOnFocusLossInFullscreen;

        private Bindable<OsuConfineMouseMode> osuConfineMode;
        private IBindable<LocalUserPlayingState> localUserPlaying;

        [BackgroundDependencyLoader]
        private void load(ILocalUserPlayInfo localUserInfo, FrameworkConfigManager frameworkConfigManager, OsuConfigManager osuConfigManager)
        {
            frameworkConfineMode = frameworkConfigManager.GetBindable<ConfineMouseMode>(FrameworkSetting.ConfineMouseMode);
            frameworkWindowMode = frameworkConfigManager.GetBindable<WindowMode>(FrameworkSetting.WindowMode);
            frameworkMinimiseOnFocusLossInFullscreen = frameworkConfigManager.GetBindable<bool>(FrameworkSetting.MinimiseOnFocusLossInFullscreen);
            frameworkWindowMode.BindValueChanged(_ => updateConfineMode());
            frameworkMinimiseOnFocusLossInFullscreen.BindValueChanged(_ => updateConfineMode());

            osuConfineMode = osuConfigManager.GetBindable<OsuConfineMouseMode>(OsuSetting.ConfineMouseMode);
            localUserPlaying = localUserInfo.PlayingState.GetBoundCopy();

            osuConfineMode.ValueChanged += _ => updateConfineMode();
            localUserPlaying.BindValueChanged(_ => updateConfineMode(), true);
        }

        private void updateConfineMode()
        {
            // confine mode is unavailable on some platforms
            if (frameworkConfineMode.Disabled)
                return;

            // To isolate the cursor, can be useful in borderless mode
            frameworkConfineMode.Value = ConfineMouseMode.Always;
        }
    }
}
