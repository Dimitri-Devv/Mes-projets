namespace Breeder;

public class FrmBtn : Button {
    public FrmBtn() {
        BackColor = System.Drawing.Color.Gray;
        Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        ForeColor = System.Drawing.Color.White;
        Location = new System.Drawing.Point(12, 12);
        Size = new System.Drawing.Size(165, 36);
    }
}

public class FrmBtnQuitter : FrmBtn {
    public FrmBtnQuitter(Form backForm) {
        Location = new System.Drawing.Point(12, 12);
        Name = "btnQuitter";
        Text = "QUITTER";
        Size = new System.Drawing.Size(165, 36);
        Click += (_, _) => Program.SwitchMainForm(backForm);
    }
}