using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDatVe.Models
{
    public class EGhe
    {
        public string maghe { get; set; }
        public int tang { get; set; }
        public string maxe { get; set; }
		public string tenghe { get; set; }
        public int vitriX { get; set; }
        public int vitriY { get; set; }
        public Nullable<bool>  active { get; set; }
        public Nullable<int> trangthai { get; set; }
    }
}