﻿namespace ZetaResourceEditor.ExtendedControlsLibrary.General.Base;

using Skinning.CustomRibbonForm;
using UIUpdating;

public class DevExpressRibbonFormBase :
    MyRibbonForm,
    IUpdateUI
{
    private UpdateUIController _uuiController;

    protected DevExpressRibbonFormBase()
    {
        // Allow for capturing the F1 key.
        KeyPreview = true;
    }

    protected IGuiEnvironment GuiHost => InternalHost;

    public static IGuiEnvironment InternalHost { get; protected set; }

    protected UpdateUIController UuiController => _uuiController ??= new(this);

    public virtual void DoUpdateUI(
        UpdateUIInformation information)
    {
        // Does nothing.
    }

    public void PerformUpdateUI(object userState = null)
    {
        if (!DesignMode)
        {
            UuiController.PerformUpdateUI(this, userState);
        }
    }

    protected override void OnLoad(
        EventArgs e)
    {
        // 2010-11-25, Uwe Keim. Experimentally.
        AutoScaleMode = AutoScaleMode.None;

        base.OnLoad(e);
    }

    protected override void OnKeyDown(
        KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e is { KeyCode: Keys.F1, Alt: false } and { Control: false, Shift: false })
        {
            ExecuteHelp();
            e.Handled = true;
        }
    }

    protected override void OnShown(
        EventArgs e)
    {
        GuiHost?.HideSplash();

        base.OnShown(e);

        PerformUpdateUI();
    }

    protected void ExecuteHelp()
    {
        GuiHost.ExecuteHelp();
    }
}