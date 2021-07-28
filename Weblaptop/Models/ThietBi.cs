namespace Weblaptop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThietBi")]
    public partial class ThietBi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThietBi()
        {
            ChiTietDonDatHangs = new HashSet<ChiTietDonDatHang>();
        }

        [Key]
        public int MaTB { get; set; }

        [Required]
        [StringLength(100)]
        public string TenTB { get; set; }

        public double? GiaBan { get; set; }

        public string Mota { get; set; }

        [StringLength(50)]
        public string Anh { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int? SoluongTon { get; set; }

        public int? MaTH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }

        public virtual ThuongHieu ThuongHieu { get; set; }
    }
}
