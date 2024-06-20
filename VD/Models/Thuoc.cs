using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VD.Models;

public class Thuoc
{
    [Key, Column(Order = 0)]
    public int ID_LV { get; set; }

    [Key, Column(Order = 1)]
    public int ID_NTD { get; set; }

    // Định nghĩa khóa ngoại tới bảng LinhVuc
    [ForeignKey("ID_LV")]
    public virtual LinhVuc LinhVuc { get; set; }

    // Định nghĩa khóa ngoại tới bảng NhaTuyenDung
    [ForeignKey("ID_NTD")]
    public virtual NhaTuyenDung NhaTuyenDung { get; set; }
}