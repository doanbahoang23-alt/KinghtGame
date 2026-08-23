# Knight Game - Action RPG Framework
Xây dựng một bộ khung dự án phát triển game RPG 2D trên nền tảng Unity 6. Dự án tập trung vào việc tìm hiểu về Unity, xây dựng kiến trúc mã nguồn sạch (OOP,SRP), Hub Pattern và tối ưu hóa Multi-Scene, dễ dang mở rộng dự án hoặc tái sử dụng tài nguyên.

**Tác giả:** Đoàn Hoàng
# Tính năng nổi bật
Kiến trúc Multi-Scence tối ưu: Áp dụng kỹ thuật tải cộng dồn (Additive Loading). Phân tách rõ giữa Core Scene(Chứa Player, UI, Camera) và Level Scenes(môi trường, quái, bẫy).

Hệ thống chỉ số: 
  - Xây dựng các lớp cơ sở độc lập có tính kế thừa: BaseHealth, BaseDamage, BaseSpeed, BaseStamina.
  - Dễ dàng tái sử dụng cho cả người chơi lẫn hệ thống quái.
Di chuyển và vật lý:
  - Áp dụng Unity Input System mới.
  - Can thiệp trực tiếp qua Rigidbody2D.linearVelocity kết hợp Vector2.normalized nhằm đồng bộ tốc độ di chuyển.
Hệ thống chiến đấu cơ bản:
  - Hoạt ảnh chiến đấu điều khiển bằng Trigger
Camera: Tích hợp Cinemachine bán sát nhân vật
# Yêu cầu hệ thống
Engine: Unity 6
Packages bắt buộc:
  - Input System
  - Cinemachine
  - 2D Tilemap Editor
# Quy tắc tổ chức dự án:
Core Scene: Chỉ chứa Player, Main Camera, EventSystem, và Canvas UI. Tuyệt đối không vẽ Map hay đặt Quái vật ở đây.

Map Scene (Level_01, Level_02...): Chỉ chứa Tilemap (Môi trường), Enemies (Quái vật), và các vật thể tương tác. Bắt buộc phải có một GameObject rỗng gắn Tag Respawn làm điểm neo cho Player khi load map.

Hub Pattern: Các logic tính toán (Máu, Thể lực, Sát thương, Tốc độ) được viết ở các script nhỏ lẻ. PlayerController đóng vai trò là "Bộ não" trung tâm điều phối các linh kiện này.

