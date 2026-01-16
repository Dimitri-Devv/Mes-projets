namespace Breeder; 

public class StyleUtils {
    public static void AppliquerTheme(Form form) {
        form.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        form.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))),
            ((int)(((byte)(64)))));
        form.WindowState = FormWindowState.Maximized;
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
    }

    public static void AppliquerBouton(Button button) {
        button.BackColor = System.Drawing.Color.Gray;
        button.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        button.ForeColor = System.Drawing.Color.White;
        button.Location = new System.Drawing.Point(12, 12);
        button.Size = new System.Drawing.Size(165, 36);
    }
    
    public static void AppliquerBoutonQuitter(Button button, Form backForm) {
        AppliquerBouton(button);
        button.Location = new System.Drawing.Point(12, 12);
        button.Name = "btnQuitter";
        button.Text = "QUITTER";
        button.Size = new System.Drawing.Size(165, 36);
        button.Click += (_, _) => Program.SwitchMainForm(backForm);
    }
}