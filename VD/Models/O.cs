using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VD.Models;

public class O
{
    [Key, Column(Order = 0)]
    public int ID_DD { get; set; }

    [Key, Column(Order = 1)]
    public int ID_NTD { get; set; }

    // Định nghĩa khóa ngoại tới bảng DiaDiem
    [ForeignKey("ID_DD")]
    public virtual DiaDiem DiaDiem { get; set; }

    // Định nghĩa khóa ngoại tới bảng NhaTuyenDung
    [ForeignKey("ID_NTD")]
    public virtual NhaTuyenDung NhaTuyenDung { get; set; }
}