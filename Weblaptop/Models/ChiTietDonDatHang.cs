namespace Weblaptop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDonDatHang")]
    public partial class ChiTietDonDatHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDH { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaTB { get; set; }

        public int? SoLuong { get; set; }

        public double? DonGia { get; set; }

        public double? TongTien { get; set; }

        public virtual DonDatHang DonDatHang { get; set; }

        public virtual ThietBi ThietBi { get; set; }
    }
}
