using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chat_service_se357.Models
{
    [Table("tb_user")]
    public class SqlUser
    {
        [Key]
        public long ID { get; set; }
        public string name { get; set; } = "";
        public string code { get; set; }  


        //public string idHub { get; set; } = "";


        public SqlConversation conversations { get; set; } // sau này serialize thành string để lưu cho dễ, tối ưu hơn link
    }
}
