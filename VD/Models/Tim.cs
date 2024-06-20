using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VD.Models;

public class Tim
{
    [Key, Column(Order = 0)]
    public int ID_UV { get; set; }

    [Key, Column(Order = 1)]
    public int ID_NTD { get; set; }

    // Định nghĩa khóa ngoại tới bảng UngVien
    [ForeignKey("ID_UV")]
    public virtual UngVien UngVien { get; set; }

    // Định nghĩa khóa ngoại tới bảng NhaTuyenDung
    [ForeignKey("ID_NTD")]
    public virtual NhaTuyenDung NhaTuyenDung { get; set; }
}