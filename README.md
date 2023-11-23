# chat-service-se357
 
 luồng: client nhấn vào nút "Chat với shop" trong màn hình 
 client sẽ GỬI 2 lần api POST createUser gồm {ID user và name} của shop và của client đó để tạo record trong db của chat-service
 sau đó client GỬI tin nhắn cho shop thì gửi api POST createMsg(senderCode, receiverCode, msg)
 response trả về true là đã ghi nhận, false là gửi thất bại