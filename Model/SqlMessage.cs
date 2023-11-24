using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chat_service_se357.Models
{
    [Table("tb_message")]
    public class SqlMessage
    {
        [Key]
        public long ID { get; set; }
        public string senderCode { get; set; }
        public string receiverCode { get; set; }
        //public DateTime time { get; set; }
        public string message { get; set; } = "";

        public SqlConversation conversations { get; set; }

    }
}
