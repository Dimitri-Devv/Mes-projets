namespace Breeder; 

public class FrmDgv : DataGridView {
    public FrmDgv() {
        BackgroundColor = Color.DimGray;
        AllowUserToAddRows = false;
        AllowUserToDeleteRows = false;
        AllowUserToResizeColumns = false;
        AllowUserToResizeRows = false;
        BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
        ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
        ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        Cursor = System.Windows.Forms.Cursors.Hand;
        ReadOnly = true;
        RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
        SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    }
}