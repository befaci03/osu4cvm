// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using osu.Game.Graphics.UserInterfaceV2;

namespace osu.Game.Overlays.Settings.Sections.General
{
    public partial class WarningSettings : SettingsSubsection
    {
        [Resolved(CanBeNull = true)]
        private OsuGame? game { get; set; }

        protected override LocalisableString Header => new LocalisableString("Notice");

        private LocalisableString Notice => new LocalisableString("This is a modified, in development, version of osu! made for CollabVM.\nPlease don't except everything working fine yet");

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRange(new Drawable[]
            {
                new SettingsItemV2(new FormCheckBox()
                {
                    Caption = new LocalisableString("Read me"),
                    Current = new Bindable<bool>(true) { Disabled = true },
                }) { Note = { Value = new SettingsNote.Data(Notice, SettingsNote.Type.Informational) } }
            });
        }
    }
}
