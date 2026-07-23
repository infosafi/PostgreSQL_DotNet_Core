using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgreSQL_DotNet_Core.Models
{
    public class accgeninf
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long acc_gen_id { get; set; }
        public string comcod { get; set; }
        public string gencode { get; set; }
        public string gendesc { get; set; }
        public string short_desc { get; set; }
        public string remarks { get; set; }
        public bool is_default { get; set; }
        public bool is_active { get; set; }
        public int created_by { get; set; }
        public accgeninf() { }
        public accgeninf(long acc_gen_id_, string comcod_, string gencode_, string gendesc_, string short_desc_, string remarks_, bool is_default_, bool is_active_, int created_by_)
        {
            this.acc_gen_id = acc_gen_id_;
            this.comcod = comcod_;
            this.gencode = gencode_;
            this.gendesc = gendesc_;
            this.short_desc = short_desc_;
            this.remarks = remarks_;
            this.is_default = is_default_;
            this.is_active = is_active_;
            this.created_by = created_by_;
        }
    }
}
