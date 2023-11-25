using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chat_service_se357.Models
{
    [Table("tb_conversation")]
    public class SqlConversation
    {
        [Key]
        public long ID { get; set; }
        public string ID_code { get; set; }
        public string clientCode { get; set; }
        public string shopCode { get; set; }
    }
}
