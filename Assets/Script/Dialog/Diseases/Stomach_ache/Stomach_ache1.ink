-> main

=== main ===
Bệnh nhân: Thầy lang ơi, dạo này tôi thấy không được khỏe lắm 
    + [Lắng nghe]
        Tôi: Bạn có thể miêu tả triệu chứng không ?
        ->listen
        
->END

=== listen ===
Bệnh nhân: Tôi bị tiểu ít, mệt mỏi, da hơi vàng, móng tay hơi nhạt.
    + [Khám bệnh]
        ->diagnose

=== diagnose ===
Tôi: Qua các triệu chứng đó có thể thấy bạn bị "Đau bụng".
Tôi: Do cơ thể bạn bị mất cân bằng nguyên tố cụ thể: 
->medicine

=== medicine ===
Tôi: Thiếu nguyên tố thủy mức độ 1, nguyên tố thổ mức độ 1.
->END