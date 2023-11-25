
 
 luồng: client nhấn vào nút "Chat với shop" trong màn hình 
 client sẽ GỬI 2 lần api POST createUser gồm {ID user và name} của shop và của client đó để tạo record trong db của chat-service
 sau đó client GỬI tin nhắn cho shop thì gửi api POST createMsg(senderCode, receiverCode, msg)
 response trả về true là đã ghi nhận, false là gửi thất bại


 Lưu ý: chỉ cho phép shop và client nhắn tin với nhau


 -giờ sửa lại cái DTO response của getListConversation, trả về tên conversation là tên của shop/client mình đang nhắn tin
 trả về thêm cái tin nhắn cuối cùng của conversation

 - ròi thêm cái time cho message
