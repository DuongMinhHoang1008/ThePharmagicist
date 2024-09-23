# ThePharmagicist
## Tổng quan
- Thể loại: RPG, phiêu lưu, giải đố
- Đối tượng hướng tới: 18 - 35 tuổi
- Chủ đề: Ma thuật và Y dược
- Bối cảnh: Một đất nước phía Đông (dựa trên nước Đại Việt thời Hậu Lê) trong một thế giới giả tưởng nơi con người có thể sử dụng ma thuật khi họ đạt tới cảnh giới nhất định trong lĩnh vực y dược, những người này được gọi là Ma Dược Sư, người chơi trong vai một Ma Dược Sư mới vào nghề, thành viên mới của tổ chức Kim Quy Hội, một hội của những Ma Dược Sư cùng lí tưởng hướng đến sức khỏe mạnh mẽ và bền vững
## Công nghệ sử dụng
- Blockchain
- Thirdweb SDK
- AI
- Unity
- Window
## Các thành phần trong game
- Player: nhân vật mà người chơi điều khiển
- NPC hỗ trợ: hỗ trợ hướng dẫn người chơi
- Token: GOLD và SILV
- Thảo dược: dùng để chế thuốc
- Thuốc: có thể dùng để chữa bệnh, một số loại có thể khai mở khả năng ma thuật của người chơi
- Trang bị: trang bị tăng hiệu quả ma thuật
## Gameplay
- Thực hiện nhiệm vụ chữa bệnh: khám bệnh và chữa bệnh
- Khám phá và thu thập thảo dược để chế thuốc nhằm chữa bệnh
- Đánh yêu quái bằng ma thuật
## Gameloop
- Nhận nhiệm vụ
- Khám bệnh
- Đi kiếm thảo dược: nhặt thảo dược, đánh quái
- Chế thuốc
- Hoàn thành nhiệm vụ: nhận phần thưởng
## Các cơ chế
- Đăng nhập vào ví điện tử
- Khám phá và thu thập
- Ngũ hành tương sinh tương khắc
- Nhiệm vụ
- Chữa bệnh
- Chế thuốc
- Ma thuật và săn quái
- Nhận trợ giúp từ trợ lí AI (chỉ chạy được trên local tại vì muốn deploy lên thì mất phí)
## Ngũ hành sinh khắc
​Các thành phần bên trong game đều ảnh hưởng bởi ngũ hành sinh khắc
​- Người chơi và quái đều có một nguyên tố.
- Người chơi dùng nguyên tố nào thì sẽ tăng sát thương cho nguyên tố đó, nếu nguyên tố của phép khắc nguyên tố của người chơi, hoặc nguyên tố người chơi khắc nguyên tố phép thì sẽ giảm sát thương
- Quái nếu nhận sát thương nguyên tố khắc nguyên tố của nó thì sẽ nhận nhiều sát thương hơn, nếu sát thương nguyên tố sinh nguyên tố của quái thì sẽ giảm sát thương
- Thảo d​ược cũng có nguyên tố, chuyên dùng để chế thuốc, khi chế thuốc, nguyên tố A sinh nguyên tố B hợp vào nhau sẽ gấp đôi mức độ của B, nếu A khắc B hoặc B khắc A, chúng sẽ triệt tiêu nhau
- Bệnh tật là sự mất cân bằng nguyên tố, sẽ c​có nguyên tố thừa hoặc thiếu
- Muốn chữa bệnh cần tạo ra thuốc có nguyên tố lấp đầy khoảng trống thiếu, còn thừa thì cần nguyên t​ố khắc để triệt tiêu
## Điều khiển
- Di chuyển bằng: WASD hoặc mũi tên
- I: mở túi đồ
- E: Nhặt đồ
- Q: Đóng mở bảng nhiệm vụ
- Chuột trái: thi triển ma thuật 1
- Chuột phải: thi triển ma thuật 2
## Tài liệu khác
- Các thông tin chi tiết có trong [doc](https://docs.google.com/document/d/11EIhdDiMrwigTtTtjQsEXdoaWoKkaqVR/edit)
- Bản build có trong itch.io [bản build](https://dmhoang.itch.io/thepharmagicist)
