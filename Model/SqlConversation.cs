using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chat_service_se357.Models
{
    [Table("tb_conversation")]
    public class SqlConversation
    {
        [Key]
        public long ID { get; set; }
        public string clientCode { get; set; }
        public string shopCode { get; set; }
        //public List<SqlMessage> messages { get; set; }// sau này serialize thành string để lưu cho dễ, tối ưu hơn link
        //hoặc không cần cũng được, vì có bao nhiêu messages đâu
        //public SqlUser user { get; set; } đéo dùng quan hệ nữa đkm
        //public List<SqlUser> users { get; set; }
    }
}
