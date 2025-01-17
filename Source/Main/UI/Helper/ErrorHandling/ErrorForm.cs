namespace ZetaResourceEditor.UI.Helper.ErrorHandling;

using Base;

public partial class ErrorForm : FormBase
{
    private readonly MemoEditScrollbarAdjuster _adjuster = new();

    private Exception _exception;

    protected override bool WantSetGlobalIcon => false;

    public ErrorForm()
    {
        InitializeComponent();

        AcceptButton = buttonContinue;
        CancelButton = buttonContinue;
    }

    public void Initialize(Exception e)
    {
        _exception = e;

        memoEdit1.Text = getEffectiveErrorMessage(e);
    }

    private static string getEffectiveErrorMessage(Exception e)
    {
        var y = e;
        while (y is TargetInvocationException or ZspSimpleFileAccessProtectorException &&
               y.InnerException != null)
        {
            y = y.InnerException;
        }

        // --

        var lines = new List<string> { y.Message };

        y = y.InnerException;
        while (y != null)
        {
            if (y is not TargetInvocationException && y is not ZspSimpleFileAccessProtectorException)
            {
                lines.Add(y.Message);
            }

            y = y.InnerException;
        }

        return string.Join(Environment.NewLine + Environment.NewLine, lines).Trim();
    }

    private void errorForm_Load(object sender, EventArgs e)
    {
        RestoreState();
        CenterToParent();

        _adjuster.Attach(memoEdit1);

        InitiallyFillLists();
        FillItemToControls();

        UpdateUI();
    }

    private void errorForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        PersistState();
    }

    private void detailedErrorsButton_ItemClick(object sender, ItemClickEventArgs e)
    {
        using var form = new TextBoxForm();
        var message = Logger.MakeTraceMessage(_exception);
        form.Initialize(message);

        form.ShowDialog(this);
    }

    private void optionsPopupMenu_BeforePopup(
        object sender,
        CancelEventArgs e)
    {
        UpdateUI();
    }

    private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
    {
        //if (XtraMessageBox.Show(
        //        this,
        //        Resources.SR_ErrorForm_button2Click_QuitTheApplication,
        //        @"Zeta Resource Editor",
        //        MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Question,
        //        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
        //{
        DialogResult = DialogResult.Abort;
        Close();
        //}
        //else
        //{
        //    DialogResult = DialogResult.None;
        //}
    }
}