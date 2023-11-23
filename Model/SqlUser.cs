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
        public string code { get; set; }  // từ ID của user bên ecommerce migrate qua


        //public string idHub { get; set; } = "";


        public List<SqlConversation> conversations { get; set; } 
    }
}
